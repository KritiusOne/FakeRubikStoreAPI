using Aplication.CustomEntities;
using Aplication.Entities;
using Aplication.QueryFilters;

namespace Aplication.Interfaces
{
    public interface IUserService<T> where T : User
    {
        PagedList<User> GetAllUsers(UserQueryFilters filters);
        User GetUserByCredentials(string email, string password);
        Task<User> NewUserRegister(User user);
        Task<User> UpdateUser(User toUpdated, int id);
        Task DeleteUser(int id);
        Task<int> UpdateUserRol(int IdUserToUpdate, int roleId);
    }
}
