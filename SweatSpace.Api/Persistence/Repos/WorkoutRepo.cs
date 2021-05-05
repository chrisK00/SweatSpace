﻿using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using SweatSpace.Api.Persistence.Responses;
using SweatSpace.Api.Persistence.Entities;
using SweatSpace.Api.Persistence.Helpers;
using SweatSpace.Api.Persistence.Interfaces;
using SweatSpace.Api.Persistence.Params;
using SweatSpace.Persistence.Business;

namespace SweatSpace.Api.Persistence.Repos
{
    internal class WorkoutRepo : IWorkoutRepo
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public WorkoutRepo(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
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

        public async Task<WorkoutResponse> GetWorkoutResponseAsync(int id)
        {
            return await _context.Workouts.ProjectTo<WorkoutResponse>(_mapper.ConfigurationProvider)
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