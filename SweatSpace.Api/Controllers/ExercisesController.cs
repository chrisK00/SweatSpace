using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SweatSpace.Api.Business.Dtos;
using SweatSpace.Api.Business.Extensions;
using SweatSpace.Api.Business.Interfaces;

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
        public async Task<IActionResult> AddExercise(ExerciseAddDto exerciseAddDto,int workoutId)
        {
            if (!await _workoutService.UserHasWorkout(User.GetUserId(),workoutId))
            {
                return Unauthorized("You dont own this workout");
            }

            await _exerciseService.AddExerciseToWorkout(exerciseAddDto, workoutId);   
            //createdatroute? create a httpget getexercises
            return NoContent();
        }

        [HttpPut]
        public async Task<IActionResult> UpdateExercise(int workoutId,ExerciseUpdateDto exerciceUpdateDto)
        {
            if (!await _workoutService.UserHasWorkout(User.GetUserId(), workoutId) && 
                !await _workoutService.ExerciseExistsOnWorkout(workoutId, exerciceUpdateDto.Id))
            {
                return Unauthorized("You dont own this exercise");
            }
          
            await _exerciseService.UpdateExercise(exerciceUpdateDto);
            return NoContent();
        }

        [HttpPost("{exerciseId}/remove-exercise")]
        public async Task<IActionResult> RemoveExercise(int workoutId, int exerciseId)
        {
            if (!await _workoutService.UserHasWorkout(User.GetUserId(), workoutId) &&
               !await _workoutService.ExerciseExistsOnWorkout(workoutId, exerciseId))
            {
                return Unauthorized("You dont own this exercise");
            }
            await _exerciseService.RemoveExerciseAsync(exerciseId);
            return NoContent();
        }
    }
}
