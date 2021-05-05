using System.Collections.Generic;
using System.Threading.Tasks;
using SweatSpace.Api.Business.Dtos;
using SweatSpace.Api.Persistence.Dtos;
using SweatSpace.Api.Persistence.Entities;
using SweatSpace.Api.Persistence.Helpers;
using SweatSpace.Api.Persistence.Params;

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
        Task AddExerciseToWorkoutAsync(AddExerciseRequest exerciseAddDto, int workoutId);       
        Task<PagedList<Exercise>> FindExercisesAsync(ExerciseParams exerciseParams);
        Task UpdateExerciseAsync(UpdateExerciseRequest exerciceUpdateDto);
        Task RemoveWorkoutExerciseAsync(int id);
        Task RemoveExerciseAsync(string name);
        Task<IEnumerable<ExerciseDto>> GetExerciseDtosForWorkoutAsync(int workoutId, WorkoutExerciseParams workoutExerciseParams);
    }
}