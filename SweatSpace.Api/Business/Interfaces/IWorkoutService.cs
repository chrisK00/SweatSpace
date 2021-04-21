using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SweatSpace.Api.Business.Dtos;
using SweatSpace.Api.Persistence.Dtos;
using SweatSpace.Api.Persistence.Entities;
using SweatSpace.Api.Persistence.Helpers;
using SweatSpace.Api.Persistence.Params;

namespace SweatSpace.Api.Business.Interfaces
{
    public interface IWorkoutService
    {
        Task<int> AddWorkoutAsync(WorkoutAddDto workoutAddDto);
        Task<WorkoutDto> GetWorkoutDtoAsync(int id);

        /// <summary>
        /// Returns a paged list of all the workout dtos
        /// </summary>
        /// <param name="workoutParams"></param>
        /// <returns></returns>
        Task<PagedList<WorkoutDto>> GetWorkoutDtosAsync(WorkoutParams workoutParams);
        Task WorkoutCompletedAsync(int workoutId);
        Task ResetWorkoutAsync(int workoutId);
        Task UpdateWorkoutAsync(int workoutId, WorkoutUpdateDto workoutUpdateDto);
        Task ToggleLikeWorkoutAsync(int workoutId, int userId);
        Task RemoveWorkoutAsync(int workoutId);
        Task<int> CopyWorkoutAsync(int workoutId, int userId);
    }
}