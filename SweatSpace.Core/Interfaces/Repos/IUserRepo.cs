using SweatSpace.Api.Persistence.Helpers;
using SweatSpace.Core.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SweatSpace.Core.Interfaces.Repos
{
    public interface IUserRepo
    {
        Task AddUserAsync(AppUser user, string password);

        Task<AppUser> GetUserByNameAsync(string userName);

        Task<AppUser> GetUserByIdAsync(int id);

        Task<IEnumerable<WeeklyStatsUserModel>> GetWeeklyStatsUserModels();
    }
}