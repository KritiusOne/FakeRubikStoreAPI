﻿using Aplication.CustomEntities;
using Aplication.Entities;
using Aplication.Exceptions;
using Aplication.Interfaces;
using Aplication.Options;

namespace Aplication.Services
{
    public class UserService<T> : IUserService<T> where T : User
    {
        private readonly IUnitOfWork<T> _unitOfWork;
        private readonly PasswordService _passwordService;
        private readonly IDirectionService _directionService;
        public UserService(IUnitOfWork<T> unitOfWork, IDirectionService directionService)
        {
            var options = new PasswordOptions();
             this._passwordService = new PasswordService(options);
            _unitOfWork = unitOfWork;
            this._directionService = directionService;
        }

        public async Task CreateUser(User user)
        {
            if(user == null)
            {
                throw new UserException("Bad request, user is null");
            }
            //First Rule
            var Address = await _directionService.CreateVoid();
            user.IdAddress = Address.Id;
            user.UserDirectionNav = Address;

            user.Password = _passwordService.Hash(user.Password);
            await _unitOfWork.UserRepository.Add(user);
            await _unitOfWork.SaveChangesAsync();
        }
        
        public PagedList<User> GetAllUsers()
        {
            var AllUsers = _unitOfWork.UserRepository.GetAll();
            var usersPagination = PagedList<User>.CreatedPagedList(AllUsers, 1, 10);
            return usersPagination;
        }
        public User GetUserByCredentials(string email, string password)
        {
            var user = _unitOfWork.UserRepository.GetUserByCredentials(email);
            if(user != null)
            {
                var checkPassword = _passwordService.Check(user.Password, password);
                if (checkPassword)
                {
                    return user;

                }else
                {
                    throw new UserException("Error in password");
                }
            }
            else
            {
                throw new UserException("User not found");
            }
        }
        public async Task<User> NewUserRegister(User user)
        {
            //First Rule
            var Address = await _directionService.CreateVoid();
            user.IdAddress = Address.Id;
            user.UserDirectionNav = Address;
            var newUser = await _unitOfWork.UserRepository.AddAndReturnUser(user);
            return newUser;
        }

    }
}
