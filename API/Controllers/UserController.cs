using API.Response;
using Aplication.CustomEntities;
using Aplication.DTOs;
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
        private readonly IUserService<User> _userService;
        private readonly IMapper _mapper;

        public UserController(IUserService<User> userService, IMapper mapper)
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
    }
}
