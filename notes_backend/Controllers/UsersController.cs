using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using notes_backend.Entities.DataTransferObjects;
using notes_backend.Services;
using notes_backend.Interfaces;

namespace notes_backend.Controllers
{
    [Route("api/users")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUsersService _usersService;

        public UsersController(IUsersService usersService)
        {
            _usersService = usersService;
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
    }
}
