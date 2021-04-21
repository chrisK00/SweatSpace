using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
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
    internal class ExerciseService : IExerciseService
    {
        private readonly IWorkoutRepo _workoutRepo;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IExerciseRepo _exerciseRepo;
        private readonly IShuffleService _shuffleService;
        private readonly ILogger<ExerciseService> _logger;

        public ExerciseService(IWorkoutRepo workoutRepo, IUnitOfWork unitOfWork, IMapper mapper, IExerciseRepo exerciseRepo,
            IShuffleService shuffleService, ILogger<ExerciseService> logger)
        {
            _workoutRepo = workoutRepo;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _exerciseRepo = exerciseRepo;
            _shuffleService = shuffleService;
            _logger = logger;
        }

        public async Task AddExerciseToWorkoutAsync(ExerciseAddDto exerciseAddDto, int workoutId)
        {
            var exercise = await _exerciseRepo.GetExerciseByNameAsync(exerciseAddDto.Name);
            var workout = await _workoutRepo.GetWorkoutByIdAsync(workoutId);

            var workoutExercise = _mapper.Map<WorkoutExercise>(exerciseAddDto);

            //make a new exercise if it doesnt already exist
            if (exercise == null)
            {
                _logger.LogInformation($"Creating a new exercise with the name:{exerciseAddDto.Name}");
                exercise = new Exercise { Name = exerciseAddDto.Name };
                await _exerciseRepo.AddExerciseAsync(exercise);
            }

            //add the exercise to the workout
            workoutExercise.Exercise = exercise;
            workout.Exercises.Add(workoutExercise);

            await _unitOfWork.SaveAllAsync();
        }

        public Task<PagedList<Exercise>> FindExercisesAsync(ExerciseParams exerciseParams)
        {
            return _exerciseRepo.GetExercisesAsync(exerciseParams);
        }

        public async Task<IEnumerable<ExerciseDto>> GetExerciseDtosForWorkoutAsync(int workoutId,
            WorkoutExerciseParams workoutExerciseParams)
        {
            var workout = await _workoutRepo.GetWorkoutDtoAsync(workoutId);
            IEnumerable<ExerciseDto> exercises;

            if (workout == null)
            {
                _logger.LogError($"Workout {workoutId} doesnt exist");
                throw new KeyNotFoundException("Workout doesnt exist");
            }

            if (workoutExerciseParams.Shuffle)
            {
                exercises = _shuffleService.ShuffleListAsync(workout.Exercises);
            }
            exercises = workout.Exercises.OrderBy(x => x.IsCompleted);
            return exercises;
        }

        public async Task RemoveExerciseAsync(int id)
        {
            var exercise = await _exerciseRepo.GetWorkoutExerciseByIdAsync(id);
            _exerciseRepo.RemoveWorkoutExercise(exercise);
            await _unitOfWork.SaveAllAsync();
        }

        public async Task UpdateExerciseAsync(ExerciseUpdateDto exerciceUpdateDto)
        {
            var exercise = await _exerciseRepo.GetWorkoutExerciseByIdAsync(exerciceUpdateDto.Id);
            _mapper.Map(exerciceUpdateDto, exercise);
            _logger.LogInformation($"Updated: {JsonSerializer.Serialize(exercise)}");
            await _unitOfWork.SaveAllAsync();
        }
    }
}
