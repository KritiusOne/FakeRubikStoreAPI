using Aplication.Entities;

namespace Aplication.Interfaces
{
    public interface IBasicEndpointService<T> where T : BaseEntity //this not are ALL ENTITIES!!!!!
    {
        IEnumerable<T> GetAll();
        Task Create(T entity);
    }
}
