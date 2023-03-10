using AutoMapper;
using Microsoft.AspNetCore.Identity;
using notes_backend.Entities.DataTransferObjects;
using notes_backend.Entities.Models;

namespace notes_backend.Services
{
    public class UserService
    {
        private readonly UserManager<User> _userManager;
        private readonly IMapper _mapper;

        public UserService(UserManager<User> userManager, IMapper mapper)
        {
            _userManager = userManager;
            _mapper = mapper;
        }

        public async Task<IdentityResult> RegisterUser(UserRegisterDTO userData)
        {
            var user = _mapper.Map<User>(userData);
            return await _userManager.CreateAsync(user, userData.Password);
        }
    }
}
