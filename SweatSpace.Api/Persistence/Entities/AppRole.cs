using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace SweatSpace.Api.Persistence.Entities
{
    public class AppRole : IdentityRole<int>
    {
        public ICollection<AppUser> Users { get; set; } = new List<AppUser>();
    }
}