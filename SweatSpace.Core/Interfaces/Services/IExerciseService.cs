using SweatSpace.Core.Entities;
using SweatSpace.Core.Helpers;
using SweatSpace.Core.Requests;
using SweatSpace.Core.Requests.Params;
using SweatSpace.Core.Responses;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SweatSpace.Core.Interfaces.Services
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