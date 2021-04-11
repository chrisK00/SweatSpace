using System.Threading.Tasks;
using SweatSpace.Api.Persistence.Entities;

namespace SweatSpace.Api.Persistence.Interfaces
{
    public interface IUserRepo
    {
        Task AddUserAsync(AppUser user, string password);

        Task<AppUser> GetUserByNameAsync(string userName);
    }
}