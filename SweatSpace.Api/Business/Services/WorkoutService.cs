using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using SweatSpace.Api.Business.Dtos;
using SweatSpace.Api.Business.Interfaces;
using SweatSpace.Api.Persistence.Entities;
using SweatSpace.Api.Persistence.Interfaces;

namespace SweatSpace.Api.Business.Services
{
    internal class WorkoutService : IWorkoutService
    {
        private readonly IWorkoutRepo _workoutRepo;
        private readonly IMapper _mapper;
        private readonly IUserRepo _userRepo;

        public WorkoutService(IWorkoutRepo workoutRepo, IMapper mapper, IUserRepo userRepo)
        {
            _workoutRepo = workoutRepo;
            _mapper = mapper;
            _userRepo = userRepo;
        }
        public async Task<int> AddWorkoutAsync(WorkoutAddDto workoutAddDto, int userId)
        {
            var workout = _mapper.Map<Workout>(workoutAddDto);
            workout.User = await _userRepo.GetUserByIdAsync(userId);
            return await _workoutRepo.AddWorkoutAsync(workout);
        }
    }
}
