using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
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

        public WorkoutService(IWorkoutRepo workoutRepo, IMapper mapper, IUserRepo userRepo, IUnitOfWork unitOfWork)
        {
            _workoutRepo = workoutRepo;
            _mapper = mapper;
            _userRepo = userRepo;
            _unitOfWork = unitOfWork;
        }

        public async Task<int> AddWorkoutAsync(WorkoutAddDto workoutAddDto, int userId)
        {
            var user = await _userRepo.GetUserByIdAsync(userId);
            var workout = _mapper.Map<Workout>(workoutAddDto);
            //we got the user from db so he is being tracked by ef
            user.Workouts.Add(workout);
            await _unitOfWork.SaveAllAsync();
            return workout.Id;
        }

        public Task<WorkoutDto> GetWorkoutDtoAsync(int id)
        {
            return _workoutRepo.GetWorkoutDtoAsync(id);
        }

        public Task<PagedList<WorkoutDto>> GetWorkoutDtos(WorkoutParams workoutParams)
        {
            return _workoutRepo.GetWorkoutsDtos(workoutParams);
        }

        public async Task ResetWorkout(int workoutId)
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

        public async Task ToggleCompleted(int workoutId)
        {
            var workout = await _workoutRepo.GetWorkoutByIdAsync(workoutId);

            //a not completed workout with remaining exercises cannot be marked completed
            if (!workout.IsCompleted && workout.Exercises.Any(e => !e.IsCompleted))
            {
                throw new AppException("All exercises must be completed");
            }

            workout.IsCompleted = !workout.IsCompleted;
            await _unitOfWork.SaveAllAsync();
        }

        public async Task UpdateWorkoutAsync(int workoutId, WorkoutUpdateDto workoutUpdateDto)
        {
            Workout workout = await _workoutRepo.GetWorkoutByIdAsync(workoutId);
            _mapper.Map(workoutUpdateDto, workout);
            await _unitOfWork.SaveAllAsync();
        }

        public async Task<bool> UserHasWorkout(int userId, int workoutId)
        {
            var workouts = await _workoutRepo.GetWorkoutsDtos(new WorkoutParams
            {
                UserId = userId,
                FilterBy = "myWorkouts"
            });
            return workouts.TotalItems > 0;
        }

        public async Task<bool> ExerciseExistsOnWorkout(int workoutId, int exerciseId)
        {
            var workout = await _workoutRepo.GetWorkoutByIdAsync(workoutId);
            return workout.Exercises.Any(e => e.Id == exerciseId);
        }
    }
}