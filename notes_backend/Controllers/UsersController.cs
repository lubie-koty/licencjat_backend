using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using notes_backend.Entities.Models;
using notes_backend.Entities.DataTransferObjects;
using notes_backend.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace notes_backend.Controllers
{
    [Route("api/users")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUsersService _usersService;
        private readonly UserManager<User> _userManager;

        public UsersController(IUsersService usersService, UserManager<User> userManager)
        {
            _usersService = usersService;
            _userManager = userManager;
        }

        [HttpPost("register")]
        public async Task<IActionResult> RegisterUser([FromBody]UserRegisterDTO userData)
        {
            if (userData == null || !ModelState.IsValid)
            {
                return BadRequest();
            }

            var res = await _usersService.RegisterUser(userData);
            if (!res.Succeeded)
            {
                return BadRequest(new ActionResponseDTO
                {
                    IsSuccessful = false,
                    Errors = res.Errors.Select(e => e.Description)
                });
            }

            return StatusCode(201);
        }

        [HttpPost("login")]
        public async Task<ActionResult> LoginUser([FromBody]UserLoginDTO userData)
        {
            var result = await _usersService.LoginUser(userData);
            if (result.IsSuccessful == false)
            {
                return Unauthorized(result);
            }

            return Ok(result);
        }

        [HttpGet("current-user-details")]
        [Authorize]
        public async Task<ActionResult<UserDetailsDTO>> GetUserDetails()
        {
            if (User.Identity == null)
            {
                return BadRequest(new UserDetailsDTO{});
            }
            var user = await _userManager.GetUserAsync(User);

            return Ok(new UserDetailsDTO
            {
                FirstName = user.FirstName ?? "",
                LastName = user.LastName ?? ""
            });
        }
    }
}
