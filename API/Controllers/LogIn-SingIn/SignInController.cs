using API.CustomClass;
using API.Response;
using Aplication.DTOs.Users;
using Aplication.Entities;
using Aplication.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace API.Controllers.LogIn_SingIn
{
    [Route("api/[controller]")]
    [ApiController]
    public class SignInController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IUserService<User> _userService;
        private readonly IConfiguration _config;
        public SignInController(IConfiguration config, IMapper map, IUserService<User> userService)
        {
            this._config = config;
            this._mapper = map;
            this._userService = userService;            
        }
        [HttpPost]
        public async Task<IActionResult> AddNewUser(CreateUserDTO userDTO)
        {
            var user = _mapper.Map<User>(userDTO);
            var newUser = await _userService.NewUserRegister(user);
            var functions = new JwtUtilsFunction(_config);
            var token = functions.GenerateToken(newUser);
            var response = new ResponseWithToken<string>(
                "The register is succes. This is the JWT",
                token,
                "Bearer");
            return Ok(response);
        }
    }
}
