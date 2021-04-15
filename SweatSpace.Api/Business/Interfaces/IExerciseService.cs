using System.Collections.Generic;
using System.Threading.Tasks;
using SweatSpace.Api.Business.Dtos;
using SweatSpace.Api.Persistence.Entities;
using SweatSpace.Api.Persistence.Helpers;
using SweatSpace.Api.Persistence.Params;

namespace SweatSpace.Api.Business.Interfaces
{
    public interface IExerciseService
    {
        /// <summary>
        /// Adds an exercise to an existing workout
        /// </summary>
        /// <param name="exerciseAddDto"></param>
        /// <param name="workoutId"></param>
        /// <returns></returns>
        Task AddExerciseToWorkout(ExerciseAddDto exerciseAddDto, int workoutId);       
        Task<PagedList<Exercise>> FindExercises(ExerciseParams exerciseParams);       
    }
}