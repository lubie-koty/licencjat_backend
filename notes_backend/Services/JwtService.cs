using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using notes_backend.Interfaces;

namespace notes_backend.Services
{
    public class JwtService : IJwtService
    {
        private readonly IConfiguration _configuration;
        private readonly IConfigurationSection _jwtConfiguration;
        private JwtSecurityTokenHandler _tokenHandler;

        public JwtService(IConfiguration configuration)
        {
            _configuration = configuration;
            _jwtConfiguration = _configuration.GetSection("JWTSettings");
            _tokenHandler = new JwtSecurityTokenHandler();
        }

        private SecurityTokenDescriptor GetTokenDescriptor(IdentityUser user)
        {
            return new SecurityTokenDescriptor
            {
                Issuer = _jwtConfiguration["validIssuer"],
                Audience = _jwtConfiguration["validAudience"],
                Expires = DateTime.Now.AddMinutes(Convert.ToDouble(_jwtConfiguration["expiryInMinutes"])),
                SigningCredentials = new SigningCredentials
                (
                    new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtConfiguration["securityKey"]!)),
                    SecurityAlgorithms.HmacSha512Signature
                ),
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim("Id", Guid.NewGuid().ToString()),
                    new Claim(JwtRegisteredClaimNames.Sub, user.Id),
                    new Claim(JwtRegisteredClaimNames.Email, user.Email),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
                })
            };
        }

        public string GetSerializedToken(IdentityUser user)
        {
            return _tokenHandler.WriteToken(_tokenHandler.CreateToken(GetTokenDescriptor(user)));
        }
    }
}
