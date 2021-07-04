using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SweatSpace.Api.Persistence.Entities;
using SweatSpace.Api.Persistence.Helpers;
using SweatSpace.Api.Persistence.Interfaces;
using SweatSpace.Api.Persistence.Params;
using SweatSpace.Persistence.Business;

namespace SweatSpace.Api.Persistence.Repos
{
    internal class ExerciseReadOnlyRepo : IExerciseReadOnlyRepo
    {

        private readonly DataContext _context;

        public ExerciseReadOnlyRepo(DataContext context)
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