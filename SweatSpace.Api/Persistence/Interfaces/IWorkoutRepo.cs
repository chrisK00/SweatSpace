using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SweatSpace.Api.Persistence.Entities;

namespace SweatSpace.Api.Persistence.Interfaces
{
    public interface IWorkoutRepo
    {
        public Task<int> AddWorkoutAsync(Workout workout);
    }
}
