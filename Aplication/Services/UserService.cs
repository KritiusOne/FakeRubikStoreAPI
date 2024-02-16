using Aplication.DTOs;
using Aplication.Entities;
using Aplication.Interfaces;

namespace Aplication.Services
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork _unitOfWork;
        public UserService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task CreateUser(User user)
        {
            await _unitOfWork.UserRepository.Add(user);
            await _unitOfWork.SaveChangesAsync();
        }

        public  async Task<IEnumerable<User>> GetAllUsers()
        {
            return await _unitOfWork.UserRepository.GetAll();
        }
    }
}
