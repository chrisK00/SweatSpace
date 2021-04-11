using System;
using System.Text;
using System.Threading.Tasks;
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
            _userManager = userManager;
        }

        public Task<AppUser> GetUserByNameAsync(string userName)
        {
            return _userManager.FindByNameAsync(userName);
        }

        public async Task AddUserAsync(AppUser user, string password)
        {
            //creates and saves user to db
            var result = await _userManager.CreateAsync(user, password);

            if (!result.Succeeded)
            {
                var sb = new StringBuilder();
                foreach (var error in result.Errors)
                {
                    sb.Append(error.Description);                   
                }
                throw new ArgumentException(sb.ToString());
            }
        }
    }
}