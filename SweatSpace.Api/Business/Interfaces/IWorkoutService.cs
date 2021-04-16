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
        Task<int> AddWorkoutAsync(WorkoutAddDto workoutAddDto, int userId);
        Task<WorkoutDto> GetWorkoutDtoAsync(int id);

        /// <summary>
        /// Checks if the user owns the specified workout
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="workoutId"></param>
        /// <returns></returns>
        Task<bool> UserHasWorkout(int userId, int workoutId);

        /// <summary>
        /// Returns a paged list of all the workout dtos
        /// </summary>
        /// <param name="workoutParams"></param>
        /// <returns></returns>
        Task<PagedList<WorkoutDto>> GetWorkoutDtos(WorkoutParams workoutParams);
        Task ToggleCompleted(int workoutId);
        Task ResetWorkout(int workoutId);
        Task UpdateWorkoutAsync(int workoutId, WorkoutUpdateDto workoutUpdateDto);
    }
}