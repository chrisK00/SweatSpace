using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SweatSpace.Api.Business.Dtos;
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
        public async Task<IActionResult> Register(UserRegisterDto userRegisterDto)
        {
            await _userService.Register(userRegisterDto);
            return NoContent();
        }

        /// <summary>
        /// Tries to login a existing user
        /// </summary>
        /// <param name="userLoginDto"></param>
        [HttpPost("login")]
        public async Task<ActionResult<UserDto>> Login(UserLoginDto userLoginDto)
        {
            return await _userService.Login(userLoginDto);
        }
    }
}