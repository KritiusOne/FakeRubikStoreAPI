using Aplication.Entities;
using Aplication.Interfaces;
using Infraestructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infraestructure.Repositories
{
    public class UserDirectionRepository : BaseRepository<UserDirection>, IUserDirectionRepoitory
    {
        public UserDirectionRepository(FakeRubikStoreContext context) : base(context) { }
        public UserDirection AddVoid(UserDirection newAddress)
        {
            base._entities.Add(newAddress);
            return newAddress;
        }

        public async Task<List<City>> CreateCities(List<City> Cities)
        {
            try
            {
                await _context.AddRangeAsync(Cities);
                await _context.SaveChangesAsync();
                return Cities;
            }catch(Exception e)
            {
                throw new Exception("Error when try create cities", e);
            }
        }

        public async Task<List<Departament>> CreateDepartments(List<Departament> departments)
        {
            try
            {
                await _context.Departaments.AddRangeAsync(departments);
                await _context.SaveChangesAsync();
                return departments;
            }catch(Exception e)
            {
                throw new Exception("Error when try create", e);
            }
        }

        public IEnumerable<UserDirection> GetAllWithUser()
        {
            return _context.Directions
                .Include(e => e.User)
                .Include(e => e.UserCity)
                    .ThenInclude(e => e.Departament)
                        .ThenInclude(e => e.Country)
                .ToList();
        }

        public UserDirection GetByIdWithUserInfo(int id)
        {
            return _context.Directions
                .Where(e => e.Id == id)
                .Include(e => e.User)
                .Include(e => e.UserCity)
                    .ThenInclude(e => e.Departament)
                        .ThenInclude(e => e.Country)
                .FirstOrDefault();
        }
    }
}
