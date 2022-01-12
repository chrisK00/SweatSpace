using Microsoft.EntityFrameworkCore;
using SweatSpace.Core.Entities;
using SweatSpace.Core.Helpers;
using SweatSpace.Core.Interfaces.Repos;
using SweatSpace.Core.Requests.Params;
using SweatSpace.Infrastructure.Data;
using System.Linq;
using System.Threading.Tasks;

namespace SweatSpace.Infrastructure.Repos
{
    internal class ExerciseReadRepo : IExerciseReadRepo
    {

        private readonly DataContext _context;

        public ExerciseReadRepo(DataContext context)
        {
            _context = context;
        }

        public async Task<PagedList<Exercise>> GetExercisesAsync(ExerciseParams exerciseParams)
        {
            var query = _context.Exercises.OrderBy(x => x.Name).AsNoTracking();

            query = !string.IsNullOrWhiteSpace(exerciseParams.Name) ? query.Where(x => x.Name.Contains(exerciseParams.Name)) : query;

            return await PagedList<Exercise>.CreateAsync(query, exerciseParams.PageNumber, exerciseParams.ItemsPerPage);
        }
    }
}