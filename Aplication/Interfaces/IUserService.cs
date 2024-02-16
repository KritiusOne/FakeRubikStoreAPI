using Aplication.DTOs;
using Aplication.Entities;
using System.Runtime.CompilerServices;

namespace Aplication.Interfaces
{
    public interface IUserService
    {
        Task<IEnumerable<User>> GetAllUsers();
        Task CreateUser(User user);
    }
}
