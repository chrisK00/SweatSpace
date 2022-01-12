using SweatSpace.Core.Entities;
using System.Threading.Tasks;

namespace SweatSpace.Core.Interfaces.Services
{
    public interface ITokenService
    {
        Task<string> CreateTokenAsync(AppUser user);
    }
}