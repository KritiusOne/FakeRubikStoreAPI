using Aplication.Entities;
using Aplication.Exceptions;
using Aplication.Interfaces;

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
            directionSearched.City = userDirection.City;
            directionSearched.State = userDirection.State;
            directionSearched.Country = userDirection.Country;
            directionSearched.Description = userDirection.Description;

            await _unitOfWork.SaveChangesAsync();
            return directionSearched;
        }
    }
}
