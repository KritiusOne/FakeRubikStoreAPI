using Aplication.Entities;

namespace Aplication.Interfaces
{
    public interface IUserService<T> where T : User
    {
        IEnumerable<User> GetAllUsers();
        Task CreateUser(User user);
    }
}
