using Aplication.Entities;
using Aplication.Interfaces;

namespace Aplication.Services
{
    public class BasicEndpointService<T> : IBasicEndpointService<T> where T : BaseEntity
    {
        private readonly IUnitOfWork<T> _unitOfWork;
        public BasicEndpointService(IUnitOfWork<T> unitOfWork)
        {
            _unitOfWork = unitOfWork;            
        }
        public IEnumerable<T> GetAll()
        {
            return _unitOfWork.BaseRepo.GetAll();
        }
        public async Task Create(T entity)
        {
            await _unitOfWork.BaseRepo.Add(entity);
            await _unitOfWork.SaveChangesAsync();
        }

       
    }
}
