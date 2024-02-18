using Aplication.Entities;

namespace Aplication.Interfaces
{
    public interface IUserService
    {
        IEnumerable<User> GetAllUsers();
        Task CreateUser(User user);
    }
}
