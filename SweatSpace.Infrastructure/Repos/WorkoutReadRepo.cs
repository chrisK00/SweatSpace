using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using SweatSpace.Api.Persistence.Interfaces;
using SweatSpace.Core.Entities;
using SweatSpace.Core.Helpers;
using SweatSpace.Core.Requests.Params;
using SweatSpace.Core.Responses;
using SweatSpace.Infrastructure.Data;
using System.Linq;
using System.Threading.Tasks;

namespace SweatSpace.Infrastructure.Repos
{
    internal class WorkoutReadRepo : IWorkoutReadRepo
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public WorkoutReadRepo(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<Workout> GetWorkoutByIdAsync(int id)
        {
            return await _context.Workouts.AsNoTracking()
                .Include(e => e.Exercises).ThenInclude(e => e.Exercise)
                .FirstOrDefaultAsync(w => w.Id == id);
        }

        public async Task<WorkoutResponse> GetWorkoutResponseAsync(int id)
        {
            return await _context.Workouts.AsNoTracking()
                .ProjectTo<WorkoutResponse>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync(w => w.Id == id);
        }

        public async Task<PagedList<WorkoutResponse>> GetWorkoutResponsesAsync(WorkoutParams workoutParams)
        {
            var query = _context.Workouts.AsQueryable().AsNoTracking();

            query = workoutParams.FilterBy switch
            {
                "myWorkouts" => query.Where(w => w.AppUserId == workoutParams.UserId)
                .OrderBy(w => w.Date).OrderBy(w => w.IsCompleted),
                "liked" => query.Where(w => w.UsersThatLiked.Any(u => u.Id == workoutParams.UserId)),
                _ => query.OrderByDescending(w => w.UsersThatLiked.Count)
            };

            var workouts = query.ProjectTo<WorkoutResponse>(_mapper.ConfigurationProvider);
            return await PagedList<WorkoutResponse>.CreateAsync(workouts, workoutParams.PageNumber, workoutParams.ItemsPerPage);
        }
    }
}