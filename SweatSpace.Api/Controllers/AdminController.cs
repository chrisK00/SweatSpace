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
        private readonly IExerciseService _exerciseService;

        public AdminController(IUserService userService, IExerciseService exerciseService)
        {
            _userService = userService;
            _exerciseService = exerciseService;
        }

        /// <summary>
        /// Gets all members with their roles
        /// </summary>
        /// <returns></returns>
        [HttpGet("members")]
        public async Task<ActionResult<IEnumerable<MemberDto>>> GetMembers()
        {
            var users = await _userService.GetMembersAsync();
            return Ok(users);
        }

        /// <summary>
        /// Changes a user's roles
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="roles"></param>
        /// <returns></returns>
        [HttpPost("roles/{userId}")]
        public async Task<IActionResult> EditRoles(int userId, [FromQuery] string roles)
        {
           await _userService.EditRolesAsync(userId, roles.Split(","));
            return NoContent();
        }

        [HttpDelete("exercises/{name}")]
        public async Task<IActionResult> RemoveExercise(string name)
        {
            await _exerciseService.RemoveExerciseAsync(name);
            return NoContent();
        }
    }
}