using API.Response;
using Aplication.CustomEntities;
using Aplication.DTOs.Users;
using Aplication.Entities;
using Aplication.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService<User> _userService;
        private readonly IMapper _mapper;

        public UserController(IUserService<User> userService, IMapper mapper)
        {
            _mapper = mapper;
            _userService = userService;
        }
        [HttpGet]
        [Authorize(Policy = "OnlyAdmins")]
        public IActionResult GetAllUser()
        {
            var users = _userService.GetAllUsers();
            var usersDTO = _mapper.Map<IEnumerable<UserDTO>>(users);
            MetaData metaData = new MetaData()
            {
                CurrentPage = users.CurrentPage,
                HasNextPage = users.hasNextPage,
                HasPreviousPage = users.hasPreviousPage,
                PageSize = users.PageSize,
                TotalCount = users.PageCount,
                TotalPage = users.TotalPages
            };
            var response = new ResponsePagination<IEnumerable<UserDTO>>(usersDTO,
                "this is the all users",
                200,
                metaData);
            return Ok(response);
        }
        [HttpDelete("id")]
        [Authorize(Policy = "OnlyAdmins")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            await _userService.DeleteUser(id);
            return Ok($"Deleted user " + id);
        }
    }
 
}
