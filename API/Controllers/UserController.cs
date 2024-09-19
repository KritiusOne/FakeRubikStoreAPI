using API.CustomClass;
using API.Response;
using Aplication.CustomEntities;
using Aplication.DTOs.Users;
using Aplication.Entities;
using Aplication.Interfaces;
using Aplication.QueryFilters;
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
        private readonly IConfiguration _config;
        public UserController(IUserService<User> userService, IMapper mapper, IConfiguration config)
        {
            _mapper = mapper;
            _userService = userService;
            _config = config;
        }
        [HttpGet]
        [Authorize(Policy = "OnlyAdmins")]
        public IActionResult GetAllUser([FromQuery] UserQueryFilters filters)
        {

            var users = _userService.GetAllUsers(filters);
            var usersDTO = _mapper.Map<IEnumerable<UserDTO>>(users);
            MetaData metaData = new MetaData()
            {
                CurrentPage = users.CurrentPage,
                HasNextPage = users.hasNextPage,
                HasPreviousPage = users.hasPreviousPage,
                PageSize = users.PageSize,
                TotalCount = users.PageCount,
                TotalPage = users.TotalPages,
                NextPageURL = "",
                PreviousPageURL = ""
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
        [HttpPut("id")]
        [Authorize]
        public async Task<IActionResult> Update(CreateUserDTO dto, int id)
        {
            var toUpdated = _mapper.Map<User>(dto);
            User UserUpdated = await _userService.UpdateUser(toUpdated, id);
            JwtUtilsFunction jwt = new JwtUtilsFunction(_config);
            string token = jwt.GenerateToken(UserUpdated);
            var response = new ResponseWithToken<string>(
                "The user was updated success",
                token,
                "Bearer");
            return Ok(response);
        }
    }
 
}
