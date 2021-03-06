using AutoMapper;
using Microsoft.Extensions.Logging;
using SweatSpace.Api.Persistence.Interfaces;
using SweatSpace.Core.Entities;
using SweatSpace.Core.Exceptions;
using SweatSpace.Core.Helpers;
using SweatSpace.Core.Interfaces.Repos;
using SweatSpace.Core.Interfaces.Services;
using SweatSpace.Core.Requests;
using SweatSpace.Core.Requests.Params;
using SweatSpace.Core.Responses;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SweatSpace.Core.Services
{
    internal class WorkoutService : IWorkoutService
    {
        private readonly IWorkoutRepo _workoutRepo;
        private readonly IMapper _mapper;
        private readonly IUserRepo _userRepo;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<WorkoutService> _logger;
        private readonly IWorkoutReadRepo _workoutReadOnlyRepo;

        public WorkoutService(IWorkoutRepo workoutRepo, IMapper mapper, IUserRepo userRepo, IUnitOfWork unitOfWork,
            ILogger<WorkoutService> logger, IWorkoutReadRepo workoutReadRepo)
        {
            _workoutRepo = workoutRepo;
            _mapper = mapper;
            _userRepo = userRepo;
            _unitOfWork = unitOfWork;
            _logger = logger;
            _workoutReadOnlyRepo = workoutReadRepo;
        }

        public async Task<WorkoutResponse> AddWorkoutAsync(AddWorkoutRequest workoutAddDto)
        {
            var workout = _mapper.Map<Workout>(workoutAddDto);

            await _workoutRepo.AddWorkoutAsync(workout);
            await _unitOfWork.SaveAllAsync();
            return _mapper.Map<WorkoutResponse>(workout);
        }

        public async Task<WorkoutResponse> GetWorkoutResponseAsync(int id)
        {
            return await _workoutReadOnlyRepo.GetWorkoutResponseAsync(id);
        }

        public async Task<PagedList<WorkoutResponse>> GetWorkoutResponsesAsync(WorkoutParams workoutParams)
        {
            return await _workoutReadOnlyRepo.GetWorkoutResponsesAsync(workoutParams);
        }

        public async Task ResetWorkoutAsync(int workoutId)
        {
            var workout = await _workoutRepo.GetWorkoutByIdAsync(workoutId);
            workout.IsCompleted = false;
            workout.Date = null;

            foreach (var exercise in workout.Exercises)
            {
                exercise.IsCompleted = false;
            }

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

        public async Task UpdateWorkoutAsync(int workoutId, UpdateWorkoutRequest workoutUpdateDto)
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

            _ = workoutToCopy ?? throw new KeyNotFoundException("Workout doesnt exist");

            var newWorkout = new Workout
            {
                Name = workoutToCopy.Name,
                AppUserId = userId,
                Exercises = workoutToCopy.Exercises.Select(we => new WorkoutExercise
                {
                    Reps = we.Reps,
                    Sets = we.Sets,
                    AppUserId = userId,
                    Exercise = we.Exercise
                }).ToList()
            };

            await _workoutRepo.AddWorkoutAsync(newWorkout);
            await _unitOfWork.SaveAllAsync();

            return newWorkout.Id;
        }
    }
}