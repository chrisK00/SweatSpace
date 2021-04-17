using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using SweatSpace.Api.Business.Dtos;
using SweatSpace.Api.Business.Interfaces;
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

        public ExerciseService(IWorkoutRepo workoutRepo, IUnitOfWork unitOfWork, IMapper mapper, IExerciseRepo exerciseRepo)
        {
            _workoutRepo = workoutRepo;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _exerciseRepo = exerciseRepo;
        }

        public async Task AddExerciseToWorkout(ExerciseAddDto exerciseAddDto, int workoutId)
        {
            var exercise = await _exerciseRepo.GetExerciseByNameAsync(exerciseAddDto.Name);
            var workout = await _workoutRepo.GetWorkoutByIdAsync(workoutId);

            var workoutExercise = _mapper.Map<WorkoutExercise>(exerciseAddDto);

            //make a new exercise if it doesnt already exist
            if (exercise == null)
            {
                exercise = new Exercise { Name = exerciseAddDto.Name };
                await _exerciseRepo.AddExerciseAsync(exercise);
            }

            //add the exercise to the workout
            workoutExercise.Exercise = exercise;
            workout.Exercises.Add(workoutExercise);

            await _unitOfWork.SaveAllAsync();
        }

        public Task<PagedList<Exercise>> FindExercises(ExerciseParams exerciseParams)
        {
            return _exerciseRepo.GetExercisesAsync(exerciseParams);
        }

        public async Task RemoveExerciseAsync(int id)
        {
            var exercise = await _exerciseRepo.GetWorkoutExerciseByIdAsync(id);
            _exerciseRepo.RemoveWorkoutExercise(exercise);
            await _unitOfWork.SaveAllAsync();
        }

        public async Task UpdateExercise(ExerciseUpdateDto exerciceUpdateDto)
        {
            var exercise = await _exerciseRepo.GetWorkoutExerciseByIdAsync(exerciceUpdateDto.Id);
            _mapper.Map(exerciceUpdateDto, exercise);
            await _unitOfWork.SaveAllAsync();
        }
    }
}
