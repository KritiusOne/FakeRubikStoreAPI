using Aplication.CustomEntities;
using Aplication.Entities;

namespace Aplication.Interfaces
{
    public interface IUserService<T> where T : User
    {
        PagedList<User> GetAllUsers();
        User GetUserByCredentials(string email, string password);
        Task<User> NewUserRegister(User user);
        Task DeleteUser(int id);
    }
}
