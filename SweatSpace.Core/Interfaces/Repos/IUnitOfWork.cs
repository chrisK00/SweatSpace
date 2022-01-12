using System.Threading.Tasks;

namespace SweatSpace.Core.Interfaces.Repos
{
    public interface IUnitOfWork
    {
        public Task SaveAllAsync();
    }
}