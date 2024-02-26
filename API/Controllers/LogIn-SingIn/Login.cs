using Aplication.CustomEntities;
using Aplication.Entities;
using Aplication.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Web.Helpers;

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
                var token = GenerateToken(user);
                return Ok(new
                {
                    token
                });
            }
            return NotFound();
        }
        private string GenerateToken(User user)
        {
            //headers
            var symmetricalSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["JWT:Key"]));
            var singInCredentials = new SigningCredentials(symmetricalSecurityKey, SecurityAlgorithms.HmacSha256);
            var headers = new JwtHeader(singInCredentials);
            //Claims
            var claims = new[]
            {
                new Claim("Id", user.Id.ToString()),
                new Claim("IdRole", user.IdRole.ToString()),
                new Claim("First_Name", user.Name),
                new Claim("Last_Name", user.SecondName),
                new Claim("email", user.Email),
                new Claim("phone", user.Phone)
            };
            //payloads
            var payloads = new JwtPayload(_config["JWT:Issuer"],
                _config["JWT:Audience"],
                claims,
                DateTime.Now,
                DateTime.UtcNow.AddMinutes(10)
                );

            //Token
            var Token = new JwtSecurityToken(headers, payloads);
            return new JwtSecurityTokenHandler().WriteToken(Token);
        }
    }
}
