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

        public JwtService(IConfiguration configuration)
        {
            _configuration = configuration;
            _jwtConfiguration = _configuration.GetSection("JWTSettings");
        }

        public SigningCredentials GetSigningCredentials()
        {
            return new SigningCredentials
            (
                new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtConfiguration.GetSection("securityKey").Value)),
                SecurityAlgorithms.HmacSha256
            );
        }

        public List<Claim> GetClaims(IdentityUser user)
        {
            return new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.Email)
            };
        }

        public JwtSecurityToken GetTokenOptions(SigningCredentials signingCredentials, List<Claim> claims)
        {
            return new JwtSecurityToken
            (
                issuer: _jwtConfiguration["validIssuer"],
                audience: _jwtConfiguration["validAudience"],
                expires: DateTime.Now.AddMinutes(Convert.ToDouble(_jwtConfiguration["expiryInMinutes"])),
                signingCredentials: signingCredentials,
                claims: claims
            );
        }
    }
}
