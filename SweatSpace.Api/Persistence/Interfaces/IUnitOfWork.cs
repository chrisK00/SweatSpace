using System.Threading.Tasks;

namespace SweatSpace.Api.Persistence.Interfaces
{
    public interface IUnitOfWork
    {
        public Task SaveAllAsync();
    }
}