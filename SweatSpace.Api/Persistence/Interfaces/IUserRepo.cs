using System.Collections.Generic;
using System.Threading.Tasks;
using SweatSpace.Api.Persistence.Responses;
using SweatSpace.Api.Persistence.Entities;

namespace SweatSpace.Api.Persistence.Interfaces
{
    public interface IUserRepo
    {
        Task AddUserAsync(AppUser user, string password);

        Task<IEnumerable<MemberResponse>> GetMemberResponsesAsync();

        Task<AppUser> GetUserByNameAsync(string userName);

        Task<AppUser> GetUserByIdAsync(int id);
    }
}