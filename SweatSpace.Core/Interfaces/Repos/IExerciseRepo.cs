using SweatSpace.Core.Entities;
using System.Threading.Tasks;

namespace SweatSpace.Core.Interfaces.Repos
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
