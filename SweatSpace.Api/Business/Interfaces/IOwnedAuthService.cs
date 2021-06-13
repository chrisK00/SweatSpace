using System.Threading.Tasks;
using SweatSpace.Api.Persistence.Entities;

namespace SweatSpace.Api.Business.Interfaces
{
    public interface IOwnedAuthService
    {
        Task OwnsAsync<T>(int entityId, int userId) where T : BaseOwnedEntity;
    }
}
