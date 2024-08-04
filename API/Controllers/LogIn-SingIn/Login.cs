using API.CustomClass;
using API.Response;
using Aplication.CustomEntities;
using Aplication.DTOs;
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
    public class Login : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IUserService<User> _userService;
        private readonly IConfiguration _config;
        public Login(IUserService<User> user, IConfiguration config, IMapper mapper)
        {
            this._mapper = mapper;
            this._userService = user;
            this._config = config;
        }
        [HttpPost]
        public IActionResult LoginUser(UserLogin login)
        {
            var user = _userService.GetUserByCredentials(login.Email, login.Password);
            if(user != null)
            {
                var functions = new JwtUtilsFunction(_config);
                var token = functions.GenerateToken(user);
                return Ok(new ResponseWithToken<string>(
                    "The login is success. This is the JWT",token, "Bearer"
                    ));
            }
            return NotFound();
        }
        
    }
}
