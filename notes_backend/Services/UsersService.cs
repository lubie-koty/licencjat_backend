using AutoMapper;
using Microsoft.AspNetCore.Identity;
using notes_backend.Entities.DataTransferObjects;
using notes_backend.Entities.Models;
using notes_backend.Interfaces;
using System.IdentityModel.Tokens.Jwt;

namespace notes_backend.Services
{
    public class UsersService : IUsersService
    {
        private readonly UserManager<User> _userManager;
        private readonly IMapper _mapper;
        private readonly IJwtService _jwtService;

        public UsersService(UserManager<User> userManager, IMapper mapper, IJwtService jwtService)
        {
            _userManager = userManager;
            _mapper = mapper;
            _jwtService = jwtService;
        }

        public async Task<IdentityResult> RegisterUser(UserRegisterDTO userData)
        {
            var user = _mapper.Map<User>(userData);
            return await _userManager.CreateAsync(user, userData.Password);
        }

        public async Task<AuthenticationDTO> LoginUser(UserLoginDTO userData)
        {
            var user = await _userManager.FindByNameAsync(userData.Email);
            if (user == null || !await _userManager.CheckPasswordAsync(user, userData.Password))
            {
                return new AuthenticationDTO { IsSuccessful = false, ErrorMessage = "Invalid password." };
            }

            var token = new JwtSecurityTokenHandler().WriteToken(
                _jwtService.GetTokenOptions(
                    _jwtService.GetSigningCredentials(),
                    _jwtService.GetClaims(user)
                )
            );
            return new AuthenticationDTO { IsSuccessful = true, Token = token };
        }
    }
}
