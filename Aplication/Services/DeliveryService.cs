using Aplication.Entities;
using Aplication.Enums;
using Aplication.Exceptions;
using Aplication.Interfaces;

namespace Aplication.Services
{
    public class DeliveryService : IDeliveryService
    {
        private readonly IUnitOfWork<Delivery> _unitOfWork;
        public DeliveryService(IUnitOfWork<Delivery> _unitOfWork)
        {
            this._unitOfWork = _unitOfWork;
        }

        public async Task UpdateState(int newState, int IdDelivery)
        {
            bool isState = existState(newState);
            if (isState)
            {
                try
                {
                    await _unitOfWork.BeginTransactionAsync();
                    var toUpdated = await _unitOfWork.DeliveryRepo.GetById(IdDelivery);
                    toUpdated.IdState = newState;
                    await _unitOfWork.SaveChangesAsync();
                    _unitOfWork.CommitTransaction();
                }catch(Exception ex)
                {
                    _unitOfWork.RollbackTransaction();
                    throw new Exception("Unknow element is fall", ex);
                }
            }
            else
            {
                throw new BaseException("This state not found");
            }
        }
        public  bool existState(int possibleState)
        {
            foreach(StatesTypes state in Enum.GetValues(typeof(StatesTypes)))
            {
                if((int)state == possibleState)
                {
                    return true;
                }
            }
            return false;
        }
    }
}
