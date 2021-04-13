using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SweatSpace.Api.Persistence.Entities;

namespace SweatSpace.Api.Persistence.Interfaces
{
    public interface IExerciseRepo
    {
        Task<Exercise> GetExerciseByNameAsync(string name);
        Task AddExerciseAsync(Exercise exercise);
    }
}
