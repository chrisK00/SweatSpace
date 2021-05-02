using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SweatSpace.Api.Persistence.Entities;
using SweatSpace.Api.Persistence.Helpers;
using SweatSpace.Api.Persistence.Interfaces;
using SweatSpace.Api.Persistence.Params;
using SweatSpace.Persistence.Business;

namespace SweatSpace.Api.Persistence.Repos
{
    internal class ExerciseRepo : IExerciseRepo
    {
        private readonly DataContext _context;

        public ExerciseRepo(DataContext context)
        {
            _context = context;
        }

        public async Task<Exercise> GetExerciseByNameAsync(string name)
        {
            return await _context.Exercises.SingleOrDefaultAsync(e => e.Name == name.ToLower());
        }

        public async Task AddExerciseAsync(Exercise exercise)
        {
            await _context.Exercises.AddAsync(exercise);
        }

        public async Task<PagedList<Exercise>> GetExercisesAsync(ExerciseParams exerciseParams)
        {
            var query = _context.Exercises.AsNoTracking();
            return await PagedList<Exercise>.CreateAsync(query, exerciseParams.PageNumber, exerciseParams.ItemsPerPage);
        }

        public async Task<WorkoutExercise> GetWorkoutExerciseByIdAsync(int id)
        {
            return await _context.WorkoutExercises.FindAsync(id);
        }

        public void RemoveExercise(Exercise exercise) =>
            _context.Exercises.Remove(exercise);

        public void RemoveWorkoutExercise(WorkoutExercise workoutExercise) =>
            _context.WorkoutExercises.Remove(workoutExercise);

    }
}