using AutoMapper;
using Microsoft.Extensions.Logging;
using SweatSpace.Api.Persistence.Interfaces;
using SweatSpace.Core.Entities;
using SweatSpace.Core.Extensions;
using SweatSpace.Core.Helpers;
using SweatSpace.Core.Interfaces.Repos;
using SweatSpace.Core.Interfaces.Services;
using SweatSpace.Core.Requests;
using SweatSpace.Core.Requests.Params;
using SweatSpace.Core.Responses;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace SweatSpace.Core.Services
{
    internal class ExerciseService : IExerciseService
    {
        private readonly IWorkoutRepo _workoutRepo;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IExerciseRepo _exerciseRepo;
        private readonly ILogger<ExerciseService> _logger;
        private readonly IExerciseReadRepo _exerciseReadRepo;
        private readonly IWorkoutReadRepo _workoutReadRepo;

        public ExerciseService(IWorkoutRepo workoutRepo, IUnitOfWork unitOfWork, IMapper mapper, IExerciseRepo exerciseRepo,
             ILogger<ExerciseService> logger, IExerciseReadRepo exerciseReadOnlyRepo,
            IWorkoutReadRepo workoutReadRepo)
        {
            _workoutRepo = workoutRepo;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _exerciseRepo = exerciseRepo;
            _logger = logger;
            _exerciseReadRepo = exerciseReadOnlyRepo;
            _workoutReadRepo = workoutReadRepo;
        }

        public async Task AddExerciseToWorkoutAsync(AddExerciseRequest exerciseAddDto, int workoutId)
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

        public async Task<PagedList<Exercise>> FindExercisesAsync(ExerciseParams exerciseParams)
        {
            return await _exerciseReadRepo.GetExercisesAsync(exerciseParams);
        }

        public async Task<IEnumerable<ExerciseResponse>> GetExerciseResponsesForWorkoutAsync(int workoutId,
            WorkoutExerciseParams workoutExerciseParams)
        {
            var workout = await _workoutReadRepo.GetWorkoutResponseAsync(workoutId);
            IEnumerable<ExerciseResponse> exercises = new List<ExerciseResponse>(workout.Exercises);

            _ = workout ?? throw new KeyNotFoundException($"Workout with the id: {workoutId} does not exist");

            if (workoutExerciseParams.Shuffle)
            {
                exercises = workout.Exercises.Shuffle();
            }

            exercises = exercises.OrderBy(x => x.IsCompleted);
            return exercises;
        }

        public async Task RemoveExerciseAsync(string name)
        {
            var exercise = await _exerciseRepo.GetExerciseByNameAsync(name);
            _ = exercise ?? throw new KeyNotFoundException($"Could not find an exercise called: {exercise.Name}");

            _exerciseRepo.RemoveExercise(exercise);
            await _unitOfWork.SaveAllAsync();
        }

        public async Task RemoveWorkoutExerciseAsync(int id)
        {
            var exercise = await _exerciseRepo.GetWorkoutExerciseByIdAsync(id);
            _exerciseRepo.RemoveWorkoutExercise(exercise);
            await _unitOfWork.SaveAllAsync();
        }

        public async Task UpdateExerciseAsync(UpdateExerciseRequest exerciceUpdateDto)
        {
            var exercise = await _exerciseRepo.GetWorkoutExerciseByIdAsync(exerciceUpdateDto.Id);
            _mapper.Map(exerciceUpdateDto, exercise);
            _logger.LogInformation($"Updated: {JsonSerializer.Serialize(exercise)}");
            await _unitOfWork.SaveAllAsync();
        }
    }
}
