using System.Collections.Generic;
using System.Threading.Tasks;
using SweatSpace.Core.Exceptions;
using SweatSpace.Core.Helpers;
using SweatSpace.Core.Requests;
using SweatSpace.Core.Requests.Params;
using SweatSpace.Core.Responses;

namespace SweatSpace.Core.Interfaces.Services
{
    public interface IWorkoutService
    {
        Task<WorkoutResponse> AddWorkoutAsync(AddWorkoutRequest addWorkoutRequest);
        Task<WorkoutResponse> GetWorkoutResponseAsync(int id);

        /// <summary>
        /// Returns a paged list of all the workout dtos
        /// </summary>
        /// <param name="workoutParams"></param>
        /// <returns></returns>
        Task<PagedList<WorkoutResponse>> GetWorkoutResponsesAsync(WorkoutParams workoutParams);

        /// <summary>
        /// Marks a workout completed if all exercises are completed
        /// </summary>
        /// <param name="workoutId"></param>
        /// <returns></returns>
        /// <exception cref="AppException"></exception>
        Task WorkoutCompletedAsync(int workoutId);
        Task ResetWorkoutAsync(int workoutId);
        Task UpdateWorkoutAsync(int workoutId, UpdateWorkoutRequest updateWorkoutRequest);

        /// <summary>
        /// Toggles a user's like on a workout
        /// </summary>
        /// <param name="workoutId"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        /// <exception cref="KeyNotFoundException"></exception>
        Task ToggleLikeWorkoutAsync(int workoutId, int userId);
        Task RemoveWorkoutAsync(int workoutId);

        /// <summary>
        /// Creates a copy of the existing workout and adds it to the workouts owned by the specified user
        /// </summary>
        /// <param name="workoutId"></param>
        /// <param name="userId"></param>
        /// <returns>The new workout id</returns>
        Task<int> CopyWorkoutAsync(int workoutId, int userId);
    }
}