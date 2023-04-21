using LenkiMicroservice.DBContexts;
using LenkiMicroservice.Interface;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace LenkiMicroservice.Repository
{
    public class TokenBuilder : ITokenBuilder
    {
        private readonly LenkiDBContext _dbContext;

        public TokenBuilder(LenkiDBContext dbContext)
        {
            _dbContext = dbContext;
        }
        public string BuildToken(string username)
        {

            var signingKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("placeholder-key-that-is-long-enough-for-sha256"));
            var signingCredentials = new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256);
            var claims = new Claim[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, username),
            };
            var jwt = new JwtSecurityToken(claims: claims, signingCredentials: signingCredentials);
            var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);

            return encodedJwt;
        }

       
    }
}
