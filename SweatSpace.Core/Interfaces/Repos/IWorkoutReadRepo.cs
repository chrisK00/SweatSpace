using SweatSpace.Core.Entities;
using SweatSpace.Core.Helpers;
using SweatSpace.Core.Requests.Params;
using SweatSpace.Core.Responses;
using System.Threading.Tasks;

namespace SweatSpace.Api.Persistence.Interfaces
{
    public interface IWorkoutReadRepo
    {
        Task<WorkoutResponse> GetWorkoutResponseAsync(int id);
        Task<Workout> GetWorkoutByIdAsync(int id);
        Task<PagedList<WorkoutResponse>> GetWorkoutResponsesAsync(WorkoutParams workoutParams);
    }
}