using Aplication.Entities;
using Aplication.Interfaces;

namespace Aplication.Services
{
    public class UserService<T> : IUserService<T> where T : User
    {
        private readonly IUnitOfWork<T> _unitOfWork;
        public UserService(IUnitOfWork<T> unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task CreateUser(User user)
        {
            await _unitOfWork.UserRepository.Add(user);
            await _unitOfWork.SaveChangesAsync();
        }

        public  IEnumerable<User> GetAllUsers()
        {
            return  _unitOfWork.UserRepository.GetAll();
        }

    }
}
