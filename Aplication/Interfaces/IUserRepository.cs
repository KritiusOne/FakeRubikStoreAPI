using Aplication.Entities;

namespace Aplication.Interfaces
{
    public interface IUserRepository : IRepository<User>
    {
        User GetUserByCredentials(string email);
    }
}
