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

        [HttpPost]
        public async Task<IActionResult> AddExerciseAsync(ExerciseAddDto exerciseAddDto,int workoutId)
        {
            if (!await _workoutService.UserHasWorkout(User.GetUserId(),workoutId))
            {
                return Unauthorized("You dont own this workout");
            }

            await _exerciseService.AddExerciseToWorkout(exerciseAddDto, workoutId);            
            return NoContent();
        }

        [HttpPost("exercise-completed")]
        public IActionResult ExerciseCompleted()
        {
            return Created("Hi", "Hi");
        }
    }
}
