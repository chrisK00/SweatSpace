using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.Extensions.Logging;
using SweatSpace.Api.Business.Dtos;
using SweatSpace.Api.Business.Exceptions;
using SweatSpace.Api.Business.Interfaces;
using SweatSpace.Api.Persistence.Dtos;
using SweatSpace.Api.Persistence.Entities;
using SweatSpace.Api.Persistence.Helpers;
using SweatSpace.Api.Persistence.Interfaces;
using SweatSpace.Api.Persistence.Params;

namespace SweatSpace.Api.Business.Services
{
    internal class WorkoutService : IWorkoutService
    {
        private readonly IWorkoutRepo _workoutRepo;
        private readonly IMapper _mapper;
        private readonly IUserRepo _userRepo;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<WorkoutService> _logger;

        public WorkoutService(IWorkoutRepo workoutRepo, IMapper mapper, IUserRepo userRepo, IUnitOfWork unitOfWork,
            ILogger<WorkoutService> logger)
        {
            _workoutRepo = workoutRepo;
            _mapper = mapper;
            _userRepo = userRepo;
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        public async Task<int> AddWorkoutAsync(WorkoutAddDto workoutAddDto)
        {
            var workout = _mapper.Map<Workout>(workoutAddDto);

            await _workoutRepo.AddWorkoutAsync(workout);
            await _unitOfWork.SaveAllAsync();
            return workout.Id;
        }

        public Task<WorkoutDto> GetWorkoutDtoAsync(int id)
        {
            return _workoutRepo.GetWorkoutDtoAsync(id);
        }

        public Task<PagedList<WorkoutDto>> GetWorkoutDtosAsync(WorkoutParams workoutParams)
        {
            return _workoutRepo.GetWorkoutsDtosAsync(workoutParams);
        }

        public async Task ResetWorkoutAsync(int workoutId)
        {
            var workout = await _workoutRepo.GetWorkoutByIdAsync(workoutId);
            workout.IsCompleted = false;
            workout.Date = null;

            foreach (var exercise in workout.Exercises)
            {
                exercise.IsCompleted = false;
            };
            await _unitOfWork.SaveAllAsync();
        }

        public async Task WorkoutCompletedAsync(int workoutId)
        {
            var workout = await _workoutRepo.GetWorkoutByIdAsync(workoutId);

            //a not completed workout with remaining exercises cannot be marked completed
            if (workout.Exercises.Any(e => !e.IsCompleted))
            {
                _logger.LogError($"Not all exercises in workout: {workoutId} are completed");
                throw new AppException("All exercises must be completed");
            }

            workout.IsCompleted = true;
            workout.TimesCompletedThisWeek++;
            await _unitOfWork.SaveAllAsync();
        }

        public async Task UpdateWorkoutAsync(int workoutId, WorkoutUpdateDto workoutUpdateDto)
        {
            Workout workout = await _workoutRepo.GetWorkoutByIdAsync(workoutId);
            _mapper.Map(workoutUpdateDto, workout);
            await _unitOfWork.SaveAllAsync();
        }

        public async Task ToggleLikeWorkoutAsync(int workoutId, int userId)
        {
            var user = await _userRepo.GetUserByIdAsync(userId);
            var workout = await _workoutRepo.GetWorkoutByIdAsync(workoutId);
            if (workout == null)
            {
                _logger.LogError($"Workout: {workoutId} was not found");
                throw new KeyNotFoundException("Workout doesnt exist");
            }

            var likedWorkout = user.LikedWorkouts.FirstOrDefault(w => w.Id == workoutId);

            //if the user has already liked the workout
            if (likedWorkout != null)
            {
                _logger.LogInformation($"Removing like from user: {userId} on workout: {workoutId}");
                user.LikedWorkouts.Remove(likedWorkout);
            }
            else
            {
                user.LikedWorkouts.Add(workout);
            }

            await _unitOfWork.SaveAllAsync();
        }

        public async Task RemoveWorkoutAsync(int workoutId)
        {
            var workout = await _workoutRepo.GetWorkoutWithLikesAsync(workoutId);
            if (workout.UsersThatLiked.Count > 0)
            {
                var deletedUser = await _userRepo.GetUserByNameAsync("deleted");
                workout.AppUserId = deletedUser.Id;
            }
            else
            {
                _workoutRepo.RemoveWorkout(workout);
            }
            await _unitOfWork.SaveAllAsync();
        }

        public async Task<int> CopyWorkoutAsync(int workoutId, int userId)
        {
            var workoutToCopy = await _workoutRepo.GetWorkoutByIdAsync(workoutId);

            var newWorkout = new Workout
            {
                Name = workoutToCopy.Name,
                AppUserId = userId,
                Exercises = workoutToCopy.Exercises.Select(e => new WorkoutExercise
                {
                    Reps = e.Reps,
                    Sets = e.Sets,
                    Exercise = e.Exercise
                }).ToList()
            };

            await _workoutRepo.AddWorkoutAsync(newWorkout);
            await _unitOfWork.SaveAllAsync();
            return newWorkout.Id;
        }
    }
}