using SweatSpace.Core.Entities;
using System.Threading.Tasks;

namespace SweatSpace.Core.Interfaces.Services
{
    public interface IOwnedAuthService
    {
        Task OwnsAsync<T>(int entityId, int userId) where T : BaseOwnedEntity;
    }
}
