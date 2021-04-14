using System.Threading.Tasks;
using SweatSpace.Api.Persistence.Dtos;
using SweatSpace.Api.Persistence.Entities;
using SweatSpace.Api.Persistence.Helpers;
using SweatSpace.Api.Persistence.Params;

namespace SweatSpace.Api.Persistence.Interfaces
{
    public interface IWorkoutRepo
    {
        Task<int> AddWorkoutAsync(Workout workout);

        Task<WorkoutDto> GetWorkoutDtoAsync(int id);

        Task<Workout> GetWorkoutByIdAsync(int id);
        Task<PagedList<WorkoutDto>> GetWorkoutsDtos(WorkoutParams workoutParams);
    }
}