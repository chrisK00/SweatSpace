using System.Threading.Tasks;
using SweatSpace.Api.Persistence.Responses;
using SweatSpace.Api.Persistence.Entities;
using SweatSpace.Api.Persistence.Helpers;
using SweatSpace.Api.Persistence.Params;

namespace SweatSpace.Api.Persistence.Interfaces
{
    public interface IWorkoutReadOnlyRepo
    {
        Task<WorkoutResponse> GetWorkoutResponseAsync(int id);
        Task<Workout> GetWorkoutByIdAsync(int id);
        Task<PagedList<WorkoutResponse>> GetWorkoutResponsesAsync(WorkoutParams workoutParams);
    }
}