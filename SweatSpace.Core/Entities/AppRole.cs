using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace SweatSpace.Core.Entities
{
    public class AppRole : IdentityRole<int>
    {
        public ICollection<AppUserRole> Users { get; set; } = new List<AppUserRole>();
    }
}