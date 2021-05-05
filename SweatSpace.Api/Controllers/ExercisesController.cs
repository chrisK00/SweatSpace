using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SweatSpace.Api.Business.Requests;
using SweatSpace.Api.Business.Interfaces;
using SweatSpace.Api.Extensions;
using SweatSpace.Api.Persistence.Responses;
using SweatSpace.Api.Persistence.Entities;
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
        private readonly IOwnedAuthService _ownedAuthService;

        public ExercisesController(IWorkoutService workoutService, IExerciseService exerciseService,
            IOwnedAuthService ownedAuthService)
        {
            _workoutService = workoutService;
            _exerciseService = exerciseService;
            _ownedAuthService = ownedAuthService;
        }

        /// <summary>
        /// Adds a exercise to the specified workout
        /// </summary>
        /// <param name="addExerciseRequest"></param>
        /// <param name="workoutId"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> AddExercise(AddExerciseRequest addExerciseRequest, int workoutId)
        {
            addExerciseRequest.AppUserId = User.GetUserId();
            await _ownedAuthService.OwnsAsync<Workout>(workoutId, addExerciseRequest.AppUserId);

            await _exerciseService.AddExerciseToWorkoutAsync(addExerciseRequest, workoutId);
            return CreatedAtRoute(nameof(GetExercises), new { workoutId }, new { workoutId });
        }

        [HttpPut]
        public async Task<IActionResult> UpdateExercise(UpdateExerciseRequest updateExerciseRequest)
        {
            await _ownedAuthService.OwnsAsync<WorkoutExercise>(updateExerciseRequest.Id, User.GetUserId());

            await _exerciseService.UpdateExerciseAsync(updateExerciseRequest);
            return NoContent();
        }

        [HttpDelete("{exerciseId}")]
        public async Task<IActionResult> RemoveExercise(int exerciseId)
        {
            await _ownedAuthService.OwnsAsync<WorkoutExercise>(exerciseId, User.GetUserId());

            await _exerciseService.RemoveWorkoutExerciseAsync(exerciseId);
            return NoContent();
        }

        [HttpGet(Name = nameof(GetExercises))]
        public async Task<ActionResult<IEnumerable<ExerciseResponse>>> GetExercises(int workoutId,
            [FromQuery] WorkoutExerciseParams workoutExerciseParams)
        {
            var exercises = await _exerciseService.GetExerciseResponsesForWorkoutAsync(workoutId, workoutExerciseParams);
            return Ok(exercises);
        }
    }
}
