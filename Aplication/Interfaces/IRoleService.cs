using Aplication.Entities;

namespace Aplication.Interfaces
{
    public interface IRoleService
    {
        IEnumerable<Role> GetAllRoles();
        Task CreateRole(Role role);
    }
}
