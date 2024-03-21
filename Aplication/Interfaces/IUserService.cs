using Aplication.CustomEntities;
using Aplication.Entities;

namespace Aplication.Interfaces
{
    public interface IUserService<T> where T : User
    {
        PagedList<User> GetAllUsers();
        Task CreateUser(User user);
        User GetUserByCredentials(string email, string password);
    }
}
