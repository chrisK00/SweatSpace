using System.Collections.Generic;
using System.Threading.Tasks;
using SweatSpace.Api.Business.Dtos;
using SweatSpace.Api.Persistence.Dtos;
using SweatSpace.Api.Persistence.Helpers;
using SweatSpace.Api.Persistence.Params;

namespace SweatSpace.Api.Business.Interfaces
{
    public interface IWorkoutService
    {
        Task<int> AddWorkoutAsync(WorkoutAddDto workoutAddDto, int userId);
        Task<WorkoutDto> GetWorkoutDtoAsync(int id);
        Task<bool> UserHasWorkout(int userId, int workoutId);
        Task<PagedList<WorkoutDto>> GetWorkoutDtos(WorkoutParams workoutParams);
    }
}