using System.Threading.Tasks;
using SweatSpace.Core.Interfaces.Repos;
using SweatSpace.Infrastructure.Data;

namespace SweatSpace.Infrastructure.Repos
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DataContext _context;

        public UnitOfWork(DataContext context)
        {
            _context = context;
        }

        public async Task SaveAllAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}