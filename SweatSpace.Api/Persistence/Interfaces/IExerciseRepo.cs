using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SweatSpace.Api.Persistence.Entities;
using SweatSpace.Api.Persistence.Helpers;
using SweatSpace.Api.Persistence.Params;

namespace SweatSpace.Api.Persistence.Interfaces
{
    public interface IExerciseRepo
    {
        Task<Exercise> GetExerciseByNameAsync(string name);
        Task<PagedList<Exercise>> GetExercisesAsync(ExerciseParams exerciseParams);
        Task AddExerciseAsync(Exercise exercise);
        Task<WorkoutExercise> GetWorkoutExerciseByIdAsync(int id);
        void RemoveWorkoutExercise(WorkoutExercise workoutExercise);
    }
}
