﻿using Aplication.DTOs;
using Aplication.Entities;
using Aplication.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IMapper _mapper;

        public UserController(IUserService userService, IMapper mapper)
        {
            _mapper = mapper;
            _userService = userService;
        }
        [HttpPost]
        public async Task<IActionResult> CreateUser(UserDTO userDTO)
        {
            var newUser = _mapper.Map<User>(userDTO);
            await _userService.CreateUser(newUser);
            return Ok();
        }
        [HttpGet]
        public IActionResult GetAllUser()
        {
            var users = _userService.GetAllUsers();
            return Ok(users);
        }
    }
}