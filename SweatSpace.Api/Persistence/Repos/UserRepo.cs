using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SweatSpace.Api.Persistence.Entities;
using SweatSpace.Api.Persistence.Interfaces;
using SweatSpace.Persistence.Business;
using SweatSpace.Api.Persistence.Helpers;
using System.Linq;

namespace SweatSpace.Api.Persistence.Repos
{
    internal class UserRepo : IUserRepo
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly DataContext _context;

        public UserRepo(UserManager<AppUser> userManager, DataContext context)
        {
            _userManager = userManager;
            _context = context;
        }

        public async Task<AppUser> GetUserByNameAsync(string userName)
        {
            return await _userManager.FindByNameAsync(userName);
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

        public async Task<AppUser> GetUserByIdAsync(int id)
        {
            return await _context.Users.Include(w => w.Workouts).Include(w => w.LikedWorkouts)
                .FirstOrDefaultAsync(u => u.Id == id);
        }

        public async Task<IEnumerable<WeeklyStatsUserModel>> GetWeeklyStatsUserModels()
        {
            return await _context.Users.Include(x => x.Workouts).Select(u => new WeeklyStatsUserModel
            {
                Email = u.Email,
                Workouts = u.Workouts
            }).ToListAsync();
        }
    }
}