using Aplication.Entities;

namespace Aplication.Interfaces
{
    public interface IUnitOfWork<T> : IDisposable where T : BaseEntity
    {
        IRepository<User> UserRepository { get; }
        IRepository<T> BaseRepo { get;  }
        void SaveChanges();
        Task SaveChangesAsync();
    }
}
