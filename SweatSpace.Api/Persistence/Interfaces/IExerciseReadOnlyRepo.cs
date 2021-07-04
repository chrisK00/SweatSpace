using System.Threading.Tasks;
using SweatSpace.Api.Persistence.Entities;
using SweatSpace.Api.Persistence.Helpers;
using SweatSpace.Api.Persistence.Params;

namespace SweatSpace.Api.Persistence.Interfaces
{
    /// <summary>
    /// Returns all items .AsNoTracking
    /// </summary>
    public interface IExerciseReadOnlyRepo
    {
        Task<PagedList<Exercise>> GetExercisesAsync(ExerciseParams exerciseParams);
    }
}
