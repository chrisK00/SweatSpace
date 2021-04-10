using System.Threading.Tasks;
using SweatSpace.Api.Persistence.Entities;

namespace SweatSpace.Api.Persistence.Interfaces
{
    public interface IUserRepo
    {
        public Task AddUserAsync(AppUser user, string password);
    }
}