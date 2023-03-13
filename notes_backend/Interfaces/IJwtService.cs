using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Identity;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace notes_backend.Interfaces
{
    public interface IJwtService
    {
        public SigningCredentials GetSigningCredentials();
        public List<Claim> GetClaims(IdentityUser user);
        public JwtSecurityToken GetTokenOptions(SigningCredentials signingCredentials, List<Claim> claims);
    }
}
