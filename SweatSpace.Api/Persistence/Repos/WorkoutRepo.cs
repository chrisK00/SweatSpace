using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SweatSpace.Api.Persistence.Entities;
using SweatSpace.Api.Persistence.Interfaces;
using SweatSpace.Persistence.Business;

namespace SweatSpace.Api.Persistence.Repos
{
    internal class WorkoutRepo : IWorkoutRepo
    {
        private readonly DataContext _context;

        public WorkoutRepo(DataContext context)
        {
            _context = context;
        }

        public async Task AddWorkoutAsync(Workout workout)
        {
            await _context.Workouts.AddAsync(workout);
        }

        public async Task<Workout> GetWorkoutByIdAsync(int id)
        {
            return await _context.Workouts.Include(e => e.Exercises).ThenInclude(e => e.Exercise)
                .FirstOrDefaultAsync(w => w.Id == id);
        }

        public void RemoveWorkout(Workout workout)
        {
            _context.Workouts.Remove(workout);
        }

        public async Task<Workout> GetWorkoutWithLikesAsync(int workoutId)
        {
            return await _context.Workouts.Include(l => l.UsersThatLiked)
                .FirstOrDefaultAsync(w => w.Id == workoutId);
        }
    }
}