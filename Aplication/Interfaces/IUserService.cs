using Aplication.CustomEntities;
using Aplication.Entities;

namespace Aplication.Interfaces
{
    public interface IUserService<T> where T : User
    {
        PagedList<User> GetAllUsers();
        User GetUserByCredentials(string email, string password);
        Task<User> NewUserRegister(User user);
        Task<User> UpdateUser(User toUpdated, int id);
        Task DeleteUser(int id);
    }
}
