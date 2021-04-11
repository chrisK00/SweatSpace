using System.Threading.Tasks;
using SweatSpace.Api.Business.Dtos;

namespace SweatSpace.Api.Business.Interfaces
{
    public interface IWorkoutService
    {
        Task<int> AddWorkoutAsync(WorkoutAddDto workoutAddDto, int userId);
    }
}