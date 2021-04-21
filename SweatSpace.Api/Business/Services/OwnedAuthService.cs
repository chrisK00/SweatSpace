using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SweatSpace.Api.Business.Interfaces;
using SweatSpace.Api.Persistence.Entities;
using SweatSpace.Persistence.Business;

namespace SweatSpace.Api.Business.Services
{
    public class OwnedAuthService : IOwnedAuthService
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
                _logger.LogError($"User {userId} does not own {entityId} of type {typeof(T)}");
                throw new UnauthorizedAccessException("You dont own this item"); 
            }
        }
    }
}
