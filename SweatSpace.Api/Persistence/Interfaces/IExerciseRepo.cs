using System.Threading.Tasks;
using SweatSpace.Api.Persistence.Entities;

namespace SweatSpace.Api.Persistence.Interfaces
{
    public interface IExerciseRepo
    {
        void RemoveExercise(Exercise exercise);
        Task<Exercise> GetExerciseByNameAsync(string name);
        Task AddExerciseAsync(Exercise exercise);
        Task<WorkoutExercise> GetWorkoutExerciseByIdAsync(int id);
        void RemoveWorkoutExercise(WorkoutExercise workoutExercise);
    }
}
