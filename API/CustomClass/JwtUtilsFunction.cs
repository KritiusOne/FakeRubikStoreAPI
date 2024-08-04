using Aplication.Entities;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace API.CustomClass
{
    public class JwtUtilsFunction
    {
        private readonly IConfiguration _config;
        public JwtUtilsFunction(IConfiguration configuration)
        {
            _config = configuration;
        }
        public string GenerateToken(User user)
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
                new Claim("IdAddress", user.IdAddress.ToString()),
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
                DateTime.UtcNow.AddHours(10)
                );

            //Token
            var Token = new JwtSecurityToken(headers, payloads);
            return new JwtSecurityTokenHandler().WriteToken(Token);
        }
    }
}
