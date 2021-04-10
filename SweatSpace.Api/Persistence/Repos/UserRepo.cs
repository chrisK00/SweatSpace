using Microsoft.AspNetCore.Identity;
using SweatSpace.Api.Persistence.Entities;
using SweatSpace.Api.Persistence.Interfaces;

namespace SweatSpace.Api.Persistence.Repos
{
    internal class UserRepo : IUserRepo
    {
        private readonly UserManager<AppUser> _userManager;
        
        public UserRepo(UserManager<AppUser> userManager)
        {
        }
    }
}