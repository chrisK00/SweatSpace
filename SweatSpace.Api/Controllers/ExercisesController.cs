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
    [Route("workouts/{workoutId}/[controller]")]
    [ApiController]
    public class ExercisesController : ControllerBase
    {
        private readonly IWorkoutService _workoutService;
        private readonly IExerciseService _exerciseService;

        public ExercisesController(IWorkoutService workoutService, IExerciseService exerciseService)
        {
            _workoutService = workoutService;
            _exerciseService = exerciseService;
        }

        /// <summary>
        /// Adds a exercise to the specified workout
        /// </summary>
        /// <param name="exerciseAddDto"></param>
        /// <param name="workoutId"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> AddExercise(ExerciseAddDto exerciseAddDto, int workoutId)
        {
            exerciseAddDto.AppUserId = User.GetUserId();
            if (!await _workoutService.UserHasWorkoutAsync(User.GetUserId(), workoutId))
            {
                return Unauthorized("You dont own this workout");
            }

            await _exerciseService.AddExerciseToWorkoutAsync(exerciseAddDto, workoutId);
            return CreatedAtRoute(nameof(GetExercises), new { workoutId }, new { workoutId });
        }

        [HttpPut]
        public async Task<IActionResult> UpdateExercise(int workoutId, ExerciseUpdateDto exerciceUpdateDto)
        {
            if (!await _workoutService.UserHasWorkoutAsync(User.GetUserId(), workoutId) &&
                !await _workoutService.ExerciseExistsOnWorkoutAsync(workoutId, exerciceUpdateDto.Id))
            {
                return Unauthorized("You dont own this exercise");
            }

            await _exerciseService.UpdateExerciseAsync(exerciceUpdateDto);
            return NoContent();
        }

        [HttpDelete("{exerciseId}")]
        public async Task<IActionResult> RemoveExercise(int workoutId, int exerciseId)
        {
            if (!await _workoutService.UserHasWorkoutAsync(User.GetUserId(), workoutId) &&
               !await _workoutService.ExerciseExistsOnWorkoutAsync(workoutId, exerciseId))
            {
                return Unauthorized("You dont own this exercise");
            }
            await _exerciseService.RemoveExerciseAsync(exerciseId);
            return NoContent();
        }

        [HttpGet(Name = nameof(GetExercises))]
        public async Task<ActionResult<IEnumerable<ExerciseDto>>> GetExercises(int workoutId,
            [FromQuery] WorkoutExerciseParams workoutExerciseParams)
        {
            var exercises = await _exerciseService.GetExerciseDtosForWorkoutAsync(workoutId, workoutExerciseParams);
            return Ok(exercises);
        }
    }
}
