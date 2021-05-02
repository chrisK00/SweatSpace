using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SweatSpace.Api.Business.Dtos;
using SweatSpace.Api.Business.Exceptions;
using SweatSpace.Api.Persistence.Dtos;
using SweatSpace.Api.Persistence.Entities;
using SweatSpace.Api.Persistence.Helpers;
using SweatSpace.Api.Persistence.Params;

namespace SweatSpace.Api.Business.Interfaces
{
    public interface IWorkoutService
    {
        Task<WorkoutDto> AddWorkoutAsync(WorkoutAddDto workoutAddDto);
        Task<WorkoutDto> GetWorkoutDtoAsync(int id);

        /// <summary>
        /// Returns a paged list of all the workout dtos
        /// </summary>
        /// <param name="workoutParams"></param>
        /// <returns></returns>
        Task<PagedList<WorkoutDto>> GetWorkoutDtosAsync(WorkoutParams workoutParams);

        /// <summary>
        /// Marks a workout completed if all exercises are completed
        /// </summary>
        /// <param name="workoutId"></param>
        /// <returns></returns>
        /// <exception cref="AppException"></exception>
        Task WorkoutCompletedAsync(int workoutId);
        Task ResetWorkoutAsync(int workoutId);
        Task UpdateWorkoutAsync(int workoutId, WorkoutUpdateDto workoutUpdateDto);

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