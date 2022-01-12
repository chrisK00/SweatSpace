using SweatSpace.Core.Entities;
using System.Threading.Tasks;

namespace SweatSpace.Core.Interfaces.Repos
{
    public interface IWorkoutRepo
    {
        Task AddWorkoutAsync(Workout workout);
        Task<Workout> GetWorkoutByIdAsync(int id);
        void RemoveWorkout(Workout workout);
        Task<Workout> GetWorkoutWithLikesAsync(int workoutId);
    }
}