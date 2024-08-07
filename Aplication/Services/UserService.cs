﻿using Aplication.CustomEntities;
using Aplication.Entities;
using Aplication.Exceptions;
using Aplication.Interfaces;
using Aplication.Options;
using System.Linq.Expressions;
using System.Text.Unicode;

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
            if (user == null)
            {
                throw new UserException("Bad request, user is null");
            }
            //check exist other users with this email
            var checkedUser = _unitOfWork.UserRepository.GetUserByCredentials(user.Email);
            if (checkedUser != null)
            {
                throw new UserException("Bad request, exist other user with this email");
            }
            //First Rule
            var Address = await _directionService.CreateVoid();
            user.IdAddress = Address.Id;
            user.AdressInfo = Address;

            //Second Rule, all user begins with role 2
            user.IdRole = 2;

            user.Password = _passwordService.Hash(user.Password);
            var newUser = await _unitOfWork.UserRepository.AddAndReturnUser(user);
            return newUser;
        }
        public async Task DeleteUser(int id)
        {
            await _unitOfWork.UserRepository.Delete(id);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task<User> UpdateUser(User toUpdated, int id)
        {
            var user = _unitOfWork.UserRepository.GetUserById(id);
            if(user == null)
            {
                throw new BaseException("Update error, user not fount");
            }
            user.Name = toUpdated.Name;
            user.SecondName = toUpdated.SecondName;
            user.Email = toUpdated.Email;
            if(toUpdated.Password != "" && toUpdated.Password != null)
            {
                bool check = _passwordService.Check(user.Password, toUpdated.Password);
                if (!check)
                {
                    user.Password = _passwordService.Hash(toUpdated.Password);
                }
            }
            user.Phone = toUpdated.Phone;
            _unitOfWork.UserRepository.Update(id, user);
            await _unitOfWork.SaveChangesAsync();
            return user;
        }
    }
}
