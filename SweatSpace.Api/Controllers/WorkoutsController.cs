using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SweatSpace.Api.Business.Dtos;
using SweatSpace.Api.Business.Extensions;
using SweatSpace.Api.Business.Interfaces;
using SweatSpace.Api.Persistence.Dtos;

namespace SweatSpace.Api.Controllers
{
    [Authorize]
    public class WorkoutsController : BaseApiController
    {
        private readonly IWorkoutService _workoutService;

        public WorkoutsController(IWorkoutService workoutService)
        {
            _workoutService = workoutService;
        }

        [HttpPost]
        public async Task<IActionResult> AddWorkout(WorkoutAddDto workoutAddDto)
        {
            var workoutId = await _workoutService.AddWorkoutAsync(workoutAddDto, User.GetUserId());
            return CreatedAtRoute(nameof(GetWorkout), new { id = workoutId }, new { id = workoutId });
        }

        [HttpGet("{id}", Name = nameof(GetWorkout))]
        public async Task<ActionResult<WorkoutDto>> GetWorkout(int id)
        {
            return await _workoutService.GetWorkoutDtoAsync(id);
        }

    }
}