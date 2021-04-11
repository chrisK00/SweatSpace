using System.Collections.Generic;
using System.Threading.Tasks;
using SweatSpace.Api.Persistence.Dtos;
using SweatSpace.Api.Persistence.Entities;

namespace SweatSpace.Api.Persistence.Interfaces
{
    public interface IUserRepo
    {
        Task AddUserAsync(AppUser user, string password);
        Task<IEnumerable<MemberDto>> GetMembersAsync();
        Task<AppUser> GetUserByNameAsync(string userName);
    }
}