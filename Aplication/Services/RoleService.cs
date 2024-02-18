using Aplication.Entities;
using Aplication.Interfaces;

namespace Aplication.Services
{
    public class RoleService : IRoleService
    {
        private readonly IUnitOfWork _unitOfWork;
        public RoleService(IUnitOfWork unit)
        {
            _unitOfWork = unit;
        }

        public IEnumerable<Role> GetAllRoles()
        {
            return _unitOfWork.RoleRepo.GetAll();
        }
        public async Task CreateRole(Role role)
        {
            await _unitOfWork.RoleRepo.Add(role);
            await _unitOfWork.SaveChangesAsync();
        }

    }
}
