using System.Threading.Tasks;
using SweatSpace.Api.Business.Dtos;

namespace SweatSpace.Api.Business.Interfaces
{
    public interface IExerciseService
    {
        /// <summary>
        /// Adds an exercise to an existing workout
        /// </summary>
        /// <param name="exerciseAddDto"></param>
        /// <param name="workoutId"></param>
        /// <returns></returns>
        Task AddExerciseToWorkout(ExerciseAddDto exerciseAddDto, int workoutId);       
    }
}