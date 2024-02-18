using Aplication.Entities;

namespace Aplication.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository<User> UserRepository { get; }
        IRepository<Role> RoleRepo { get; }
        void SaveChanges();

        Task SaveChangesAsync();
    }
}
