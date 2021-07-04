using System.Collections.Generic;
using System.Threading.Tasks;
using SweatSpace.Api.Persistence.Responses;

namespace SweatSpace.Api.Persistence.Interfaces
{
    public interface IUserReadOnlyRepo
    {
        Task<IEnumerable<MemberResponse>> GetMemberResponsesAsync();
    }
}