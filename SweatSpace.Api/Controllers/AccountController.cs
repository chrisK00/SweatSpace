using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SweatSpace.Api.Business.Requests;
using SweatSpace.Api.Business.Interfaces;

namespace SweatSpace.Api.Controllers
{
    public class AccountController : BaseApiController
    {
        private readonly IUserService _userService;

        public AccountController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterUserRequest registerUserRequest)
        {
            await _userService.RegisterAsync(registerUserRequest);
            return NoContent();
        }

        /// <summary>
        /// Tries to login a existing user
        /// </summary>
        /// <param name="loginUserRequest"></param>
        [HttpPost("login")]
        public async Task<ActionResult<string>> Login(LoginUserRequest loginUserRequest)
        {
            var token = await _userService.LoginAsync(loginUserRequest);
            return Ok(new { Token = token });
        }
    }
}