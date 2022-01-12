using System.Collections.Generic;
using System.Threading.Tasks;
using SweatSpace.Core.Responses;

namespace SweatSpace.Core.Interfaces.Repos
{
    public interface IUserReadRepo
    {
        Task<IEnumerable<MemberResponse>> GetMemberResponsesAsync();
    }
}