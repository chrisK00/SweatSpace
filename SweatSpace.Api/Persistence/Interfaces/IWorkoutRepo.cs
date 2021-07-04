using System.Collections.Generic;
using System.Threading.Tasks;
using SweatSpace.Api.Persistence.Entities;

namespace SweatSpace.Api.Persistence.Interfaces
{
    public interface IWorkoutRepo
    {
        Task AddWorkoutAsync(Workout workout);
        Task<Workout> GetWorkoutByIdAsync(int id);
        void RemoveWorkout(Workout workout);
        Task<Workout> GetWorkoutWithLikesAsync(int workoutId);
    }
}