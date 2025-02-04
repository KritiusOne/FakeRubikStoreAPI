using Aplication.CustomEntities;
using Aplication.Entities;
using Aplication.Exceptions;
using Aplication.Interfaces;
using System.Text.Json.Serialization;

namespace Aplication.Services
{
    public class DirectionService : IDirectionService
    {
        private readonly IUnitOfWork<UserDirection> _unitOfWork;
        public DirectionService(IUnitOfWork<UserDirection> unitOfWork)
        {
            this._unitOfWork = unitOfWork;
        }
        public async Task<UserDirection> CreateVoid()
        {
            var direction = new UserDirection();
            direction = _unitOfWork.AddressRepo.AddVoid(direction);
            await _unitOfWork.SaveChangesAsync();
            return direction;
        }

        public IEnumerable<UserDirection> GetAll()
        {
            var AllAddress = _unitOfWork.AddressRepo.GetAllWithUser();
            return AllAddress;
        }
        public UserDirection GetById(int id)
        {
            var searched = _unitOfWork.AddressRepo.GetByIdWithUserInfo(id);
            return searched;
        }

        public async Task<UserDirection> Update(int id, UserDirection userDirection)
        {
            var directionSearched = _unitOfWork.AddressRepo.GetByIdWithUserInfo(id);
            if (directionSearched == null)
            {
                throw new BaseException("Not found");
            }
            directionSearched.Address = userDirection.Address;
            directionSearched.IdCity = 1;
            directionSearched.Description = userDirection.Description;

            await _unitOfWork.SaveChangesAsync();
            return directionSearched;
        }
        public async Task<string> GetExternalCities()
        {
            string token = "";
            var client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
            HttpResponseMessage responseMessage = await client.GetAsync("https://api-sandbox.factus.com.co/v1/municipalities");
            if (!responseMessage.IsSuccessStatusCode)
            {
                return "Error";
            }
            string json = await responseMessage.Content.ReadAsStringAsync();
            return json;
        }

        public async Task CreateExternalCitiesAndDepartament(List<ExternalMunicipalities> externals)
        {
            await _unitOfWork.BeginTransactionAsync();
            try
            {
                List<string> Namedepartments = externals.GroupBy(e => e.Department)
                                            .Select(e => e.Key)
                                            .ToList();
                List<Departament> departaments = new List<Departament>();
                foreach (var item in Namedepartments)
                {
                    departaments.Add(new Departament
                    {
                        IdCountry = 2,
                        Name = item
                    });
                    Console.WriteLine(item);
                }
                List<Departament> newDepartments = await _unitOfWork.AddressRepo.CreateDepartments(departaments);
                List<City> cities = externals.Select(e => new City
                {
                    IdDepartament = newDepartments.Find(x => x.Name == e.Department).Id,
                    Name = e.Name
                }).ToList();
                await _unitOfWork.AddressRepo.CreateCities(cities);
                _unitOfWork.CommitTransaction();
            }
            catch (Exception e)
            {
                _unitOfWork.RollbackTransaction();
                throw new Exception("Error en el mapeo", e);
            }
        }
    }
}
