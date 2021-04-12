using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SweatSpace.Api.Business.Dtos;
using SweatSpace.Api.Business.Extensions;
using SweatSpace.Api.Business.Interfaces;

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
        public async Task<ActionResult<int>> AddWorkout(WorkoutAddDto workoutAddDto)
        {
            var workoutId = await _workoutService.AddWorkoutAsync(workoutAddDto, User.GetUserId());
            return CreatedAtRoute(nameof(GetWorkout), new { id = workoutId }, new { id = workoutId });
        }

        [HttpGet("{id}", Name = nameof(GetWorkout))]
        public async Task<ActionResult> GetWorkout(int id)
        {
            return Ok("");
        }
    }
}