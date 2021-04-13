using System.Threading.Tasks;
using SweatSpace.Api.Persistence.Dtos;
using SweatSpace.Api.Persistence.Entities;

namespace SweatSpace.Api.Persistence.Interfaces
{
    public interface IWorkoutRepo
    {
        Task<int> AddWorkoutAsync(Workout workout);

        Task<WorkoutDto> GetWorkoutDtoAsync(int id);

        Task<Workout> GetWorkoutByIdAsync(int id);
    }
}