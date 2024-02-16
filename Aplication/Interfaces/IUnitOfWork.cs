using Aplication.Entities;

namespace Aplication.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository<User> UserRepository { get; }

        void SaveChanges();

        Task SaveChangesAsync();
    }
}
