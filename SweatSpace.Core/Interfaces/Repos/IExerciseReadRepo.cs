using SweatSpace.Core.Entities;
using SweatSpace.Core.Helpers;
using SweatSpace.Core.Requests.Params;
using System.Threading.Tasks;

namespace SweatSpace.Core.Interfaces.Repos
{
    /// <summary>
    /// Returns all items .AsNoTracking
    /// </summary>
    public interface IExerciseReadRepo
    {
        Task<PagedList<Exercise>> GetExercisesAsync(ExerciseParams exerciseParams);
    }
}
