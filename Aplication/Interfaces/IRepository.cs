using Aplication.Entities;

namespace Aplication.Interfaces
{
    public interface IRepository<T> where T : BaseEntity
    {
        IEnumerable<T> GetAll();
        Task<T> GetById(int id);
        Task Add(T element);
        void Update(int id, T element);
        Task Delete(int id);
    }
}
