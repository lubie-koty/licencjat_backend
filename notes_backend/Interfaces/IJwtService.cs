using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Identity;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace notes_backend.Interfaces
{
    public interface IJwtService
    {
        public string GetSerializedToken(IdentityUser user);
    }
}
