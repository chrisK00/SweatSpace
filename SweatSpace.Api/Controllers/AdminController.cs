using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SweatSpace.Api.Business.Interfaces;
using SweatSpace.Api.Persistence.Dtos;

namespace SweatSpace.Api.Controllers
{
    [Authorize(Policy = "RequireAdminRole")]
    public class AdminController : BaseApiController
    {
        private readonly IUserService _userService;

        public AdminController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet("members")]
        public async Task<ActionResult<IEnumerable<MemberDto>>> GetMembers()
        {
            var users = await _userService.GetMembersAsync();
            return Ok(users);
        }

        [HttpPost("roles/{userId}")]
        public async Task<IActionResult> EditRoles(int userId, [FromQuery] string roles)
        {
           await _userService.EditRolesAsync(userId, roles.Split(","));
            return NoContent();
        }
    }
}