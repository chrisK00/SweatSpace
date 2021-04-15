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
        Task<PagedList<Exercise>> GetExercises(ExerciseParams exerciseParams);
        Task AddExerciseAsync(Exercise exercise);
    }
}
