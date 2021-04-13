using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SweatSpace.Api.Business.Dtos;
using SweatSpace.Api.Business.Interfaces;
using SweatSpace.Api.Persistence.Interfaces;

namespace SweatSpace.Api.Business.Services
{
    internal class ExerciseService : IExerciseService
    {
        private readonly IWorkoutRepo _workoutRepo;
        private readonly IUnitOfWork _unitOfWork;

        public ExerciseService(IWorkoutRepo workoutRepo, IUnitOfWork unitOfWork)
        {
            _workoutRepo = workoutRepo;
            _unitOfWork = unitOfWork;
        }

        public Task AddExerciseToWorkout(ExerciseAddDto exerciseAddDto, int workoutId)
        {
            return Task.CompletedTask;
            //see if an exercise with this name exists in the db
            //  var exercise = exerciserepo.getexercisebyname
            //map exerciseadddto to workoutexercise
            //if exercise null make new one with the name and add it to repository tolower name then chain it on the workoutexercise
            //else just add it to the workoutexercise            
            //   var workout = _workoutRepo.getworkoutbyid
            //workout.add workoutexercise
            //savechanges
        }
    }
}
