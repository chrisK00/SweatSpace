using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SweatSpace.Api.Business.Dtos;
using SweatSpace.Api.Business.Interfaces;
using SweatSpace.Api.Extensions;
using SweatSpace.Api.Persistence.Dtos;
using SweatSpace.Api.Persistence.Entities;
using SweatSpace.Api.Persistence.Params;

namespace SweatSpace.Api.Controllers
{
    [Authorize]
    public class WorkoutsController : BaseApiController
    {
        private readonly IWorkoutService _workoutService;
        private readonly IOwnedAuthService _ownedAuthService;

        public WorkoutsController(IWorkoutService workoutService, IOwnedAuthService ownedAuthService)
        {
            _workoutService = workoutService;
            _ownedAuthService = ownedAuthService;
        }

        [HttpPost]
        public async Task<IActionResult> AddWorkout(WorkoutAddDto workoutAddDto)
        {
            workoutAddDto.AppUserId = User.GetUserId();
            var workout = await _workoutService.AddWorkoutAsync(workoutAddDto);
            return CreatedAtRoute(nameof(GetWorkout), new { id = workout.Id }, workout);
        }

        [HttpGet("{id}", Name = nameof(GetWorkout))]
        public async Task<ActionResult<WorkoutDto>> GetWorkout(int id)
        {
            return await _workoutService.GetWorkoutDtoAsync(id);
        }

        /// <summary>
        /// Gets workouts paginated
        /// </summary>
        /// <param name="workoutParams"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<WorkoutDto>>> GetWorkouts([FromQuery] WorkoutParams workoutParams)
        {
            workoutParams.UserId = User.GetUserId();
            var workouts = await _workoutService.GetWorkoutDtosAsync(workoutParams);
            Response.AddPaginationHeader(workouts.TotalItems, workouts.ItemsPerPage, workouts.PageNumber, workouts.TotalPages);
            return workouts;
        }

        /// <summary>
        /// Toggles the completed state of a workout
        /// </summary>
        /// <param name="workoutId"></param>
        /// <returns></returns>
        [HttpPost("{workoutId}/completed")]
        public async Task<IActionResult> WorkoutCompleted(int workoutId)
        {
            await _ownedAuthService.OwnsAsync<Workout>(workoutId, User.GetUserId());

            await _workoutService.WorkoutCompletedAsync(workoutId);
            return NoContent();
        }

        [HttpPost("{workoutId}/reset")]
        public async Task<IActionResult> ResetWorkout(int workoutId)
        {
            await _ownedAuthService.OwnsAsync<Workout>(workoutId, User.GetUserId());

            await _workoutService.ResetWorkoutAsync(workoutId);
            return NoContent();
        }

        /// <summary>
        /// Fully updates an existing workout
        /// </summary>
        /// <param name="workoutId"></param>
        /// <param name="workoutUpdateDto"></param>
        /// <returns></returns>
        [HttpPut("{workoutId}")]
        public async Task<IActionResult> UpdateWorkout(int workoutId, WorkoutUpdateDto workoutUpdateDto)
        {
            await _ownedAuthService.OwnsAsync<Workout>(workoutId, User.GetUserId());
            await _workoutService.UpdateWorkoutAsync(workoutId, workoutUpdateDto);
            return NoContent();
        }

        [HttpPost("{workoutId}/toggle-like")]
        public async Task<IActionResult> ToggleLikeWorkout(int workoutId)
        {
            await _workoutService.ToggleLikeWorkoutAsync(workoutId, User.GetUserId());
            return NoContent();
        }

        [HttpDelete("{workoutId}")]
        public async Task<IActionResult> RemoveWorkout(int workoutId)
        {
            await _ownedAuthService.OwnsAsync<Workout>(workoutId, User.GetUserId());

            await _workoutService.RemoveWorkoutAsync(workoutId);
            return NoContent();
        }

        [HttpPost("{workoutId}/copy")]
        public async Task<IActionResult> CopyWorkout(int workoutId)
        {
            var newWorkoutId = await _workoutService.CopyWorkoutAsync(workoutId, User.GetUserId());
            return CreatedAtRoute(nameof(GetWorkout), new { id = newWorkoutId }, new { id = newWorkoutId });
        }
    }
}