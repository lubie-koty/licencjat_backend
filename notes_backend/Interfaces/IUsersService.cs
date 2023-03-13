using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using notes_backend.Entities.DataTransferObjects;

namespace notes_backend.Interfaces
{
    public interface IUsersService
    {
        public Task<IdentityResult> RegisterUser(UserRegisterDTO userData);
        public Task<AuthenticationDTO> LoginUser(UserLoginDTO userData);
    }
}
