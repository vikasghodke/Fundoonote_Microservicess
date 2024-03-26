using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using User_Microservices.UserEntity;

namespace User_Microservices.Jwt_Token
{
    public class Jwt_Token1
    {
        private readonly IConfiguration _config;

        public Jwt_Token1(IConfiguration config)
        {
            _config = config;
        }
        public string GenereateToken(UserEntity1 userEntity1)
        {
            var secirityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var credentials = new SigningCredentials(secirityKey, SecurityAlgorithms.HmacSha256);
            var Claims = new[]
            {
                new Claim(ClaimTypes.Email, userEntity1.Email),
                new Claim(ClaimTypes.NameIdentifier, userEntity1.Id.ToString()),
            };

            var Token = new JwtSecurityToken(_config["Jwt_issuer"],
                _config["Jwt_issuer"],
                Claims,
                expires: DateTime.Now.AddMinutes(30),
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(Token);
                
                
        }
        public string GenerateTokenReset(string Email, int userid)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["jwt:key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(ClaimTypes.Email,Email),
                new Claim(ClaimTypes.NameIdentifier, userid.ToString())
                
                //new Claim(ClaimTypes.NameIdentifier,Notes.id.TOString())
            };
            var token = new JwtSecurityToken(_config["Jwt:Issuer"],
              _config["Jwt:Issuer"],
              claims
              ,
              expires: DateTime.Now.AddMinutes(15),
              signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
