using System.Collections.Generic;
using System.Threading.Tasks;
using SweatSpace.Api.Business.Requests;
using SweatSpace.Api.Persistence.Responses;
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
        /// <param name="addExerciseRequest"></param>
        /// <param name="workoutId"></param>
        /// <returns></returns>
        Task AddExerciseToWorkoutAsync(AddExerciseRequest addExerciseRequest, int workoutId);       
        Task<PagedList<Exercise>> FindExercisesAsync(ExerciseParams exerciseParams);
        Task UpdateExerciseAsync(UpdateExerciseRequest updateExerciseRequest);
        Task RemoveWorkoutExerciseAsync(int id);
        Task RemoveExerciseAsync(string name);
        Task<IEnumerable<ExerciseResponse>> GetExerciseResponsesForWorkoutAsync(int workoutId, WorkoutExerciseParams workoutExerciseParams);
    }
}