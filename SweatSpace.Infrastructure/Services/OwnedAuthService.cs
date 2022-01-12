using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SweatSpace.Core.Entities;
using SweatSpace.Core.Interfaces.Services;
using SweatSpace.Infrastructure.Data;
using System;
using System.Threading.Tasks;

namespace SweatSpace.Infrastructure.Services
{
    internal class OwnedAuthService : IOwnedAuthService
    {
        private readonly DataContext _context;
        private readonly ILogger<OwnedAuthService> _logger;

        public OwnedAuthService(DataContext context, ILogger<OwnedAuthService> logger)
        {
            _context = context;
            _logger = logger;
        }
        public async Task OwnsAsync<T>(int entityId, int userId) where T : BaseOwnedEntity
        {
            bool exists = await _context.Set<T>()
                .AnyAsync(x => x.Id == entityId && x.AppUserId == userId);

            if (!exists)
            {
                _logger.LogError($"User: {userId} does not own: {entityId} of type {typeof(T).Name}");
                throw new UnauthorizedAccessException("You dont own this item");
            }
        }
    }
}
