using LenkiMicroservice.DBContexts;
using LenkiMicroservice.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace LenkiMicroservice.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorizationController : ControllerBase
    {
        public IConfiguration _configuration;
        private readonly LenkiDBContext _context;
        public AuthorizationController(IConfiguration config, LenkiDBContext context)
        {
            _configuration = config;
            _context = context;
        }

        /// <summary>
        /// Get Authorization Token to Access other End Point
        /// </summary>
        [SwaggerOperation("Get Authorization Token to Access other End Point")]
        [HttpPost("login")]
        public IActionResult Post(Login _user)
        {

            if (_user != null && _user.UserName != null && _user.Password != null)
            {
                var user = GetUser(_user.UserName, _user.Password);

                if (user != null)
                {
                    //create claims details based on the user information
                    var claims = new[] {
                    new Claim(JwtRegisteredClaimNames.Sub, _configuration["JwtConfig:Subject"]),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString()),
                    new Claim("Id", user.Id.ToString()),
                    new Claim("FullName", user.FullName),
                    new Claim("Email", user.Email),
                    new Claim("Phone", user.Phone),
                    new Claim("UserRole", user.UserRole)
                   };

                    var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JwtConfig:Key"]));

                    var signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

                    var token = new JwtSecurityToken(_configuration["JwtConfig:Issuer"], _configuration["JwtConfig:Audience"], claims, expires: DateTime.UtcNow.AddDays(1), signingCredentials: signIn);


                    return Ok(new
                    {
                        FullName = user.FullName,
                        Email = user.Email,
                        Phone = user.Phone,
                        UserRole = user.UserRole,
                        token = new JwtSecurityTokenHandler().WriteToken(token),
                        Start = token.ValidFrom,
                        expiration = token.ValidTo,

                    });
                }
                else
                {
                    return BadRequest("Invalid credentials");
                }
            }
            else
            {
                return BadRequest();
            }
        }

        private Users GetUser(string username, string password)
        {
            return _context.Users.FirstOrDefault(u => u.Email == username && u.Password == password);
        }
    }
}
