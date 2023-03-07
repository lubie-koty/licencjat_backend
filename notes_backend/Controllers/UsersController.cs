using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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
    }
}
