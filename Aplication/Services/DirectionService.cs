using Aplication.Entities;
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
    }
}
