using SweatSpace.Api.Persistence.Entities;

namespace SweatSpace.Api.Business.Interfaces
{
    public interface ITokenService
    {
        string CreateToken(AppUser user);
    }
}
