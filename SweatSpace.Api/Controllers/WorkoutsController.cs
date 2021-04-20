using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SweatSpace.Api.Business.Dtos;
using SweatSpace.Api.Business.Interfaces;
using SweatSpace.Api.Extensions;
using SweatSpace.Api.Persistence.Dtos;
using SweatSpace.Api.Persistence.Params;

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

        [HttpGet]
        public async Task<ActionResult<IEnumerable<WorkoutDto>>> GetWorkouts([FromQuery]WorkoutParams workoutParams)
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
            if (!await _workoutService.UserHasWorkoutAsync(User.GetUserId(), workoutId))
            {
                return Unauthorized("You dont own this workout");
            }

            await _workoutService.WorkoutCompletedAsync(workoutId);
            return NoContent();
        }

        [HttpPost("{workoutId}/reset")]
        public async Task<IActionResult> ResetWorkout(int workoutId)
        {
            if (!await _workoutService.UserHasWorkoutAsync(User.GetUserId(), workoutId))
            {
                return Unauthorized("You dont own this workout");
            }

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
            if (!await _workoutService.UserHasWorkoutAsync(User.GetUserId(), workoutId))
            {
                return Unauthorized("You dont own this workout");
            }
            await _workoutService.UpdateWorkoutAsync(workoutId, workoutUpdateDto);
            return NoContent();
        }

        [HttpPost("{workoutId}/toggle-like")]
        public async Task<IActionResult> ToggleLikeWorkout(int workoutId)
        {            
            await _workoutService.ToggleLikeWorkout(workoutId, User.GetUserId());
            return NoContent();
        }
    }
}