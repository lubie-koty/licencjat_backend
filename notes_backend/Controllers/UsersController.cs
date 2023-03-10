using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using notes_backend.Entities.DataTransferObjects;
using notes_backend.Services;

namespace notes_backend.Controllers
{
    [Route("api/users")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly UserService _userService;

        public UsersController(UserService userService)
        {
            _userService = userService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> RegisterUser([FromBody]UserRegisterDTO userData)
        {
            if (userData == null || !ModelState.IsValid)
            {
                return BadRequest();
            }

            var res = await _userService.RegisterUser(userData);
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
    }
}
