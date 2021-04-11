using System.Threading.Tasks;
using SweatSpace.Api.Persistence.Entities;

namespace SweatSpace.Api.Business.Interfaces
{
    public interface ITokenService
    {
        Task<string> CreateToken(AppUser user);
    }
}
