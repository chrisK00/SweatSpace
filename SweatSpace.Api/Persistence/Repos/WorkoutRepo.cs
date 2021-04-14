﻿using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using SweatSpace.Api.Persistence.Dtos;
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

        public async Task<int> AddWorkoutAsync(Workout workout)
        {
            await _context.Workouts.AddAsync(workout);
            return workout.Id;
        }

        public Task<Workout> GetWorkoutByIdAsync(int id)
        {
            return _context.Workouts.FirstOrDefaultAsync(w => w.Id == id);
        }

        public async Task<WorkoutDto> GetWorkoutDtoAsync(int id)
        {
            return await _context.Workouts.ProjectTo<WorkoutDto>(_mapper.ConfigurationProvider)
                 .FirstOrDefaultAsync(w => w.Id == id);
        }

        public Task<PagedList<WorkoutDto>> GetWorkoutsDtos(WorkoutParams workoutParams)
        {
            var query = _context.Workouts.ProjectTo<WorkoutDto>(_mapper.ConfigurationProvider).AsNoTracking();

            return PagedList<WorkoutDto>.CreateAsync(query, workoutParams.PageNumber, workoutParams.ItemsPerPage);
        }
    }
}