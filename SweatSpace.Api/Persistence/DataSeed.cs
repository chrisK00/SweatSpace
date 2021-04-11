using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using SweatSpace.Api.Persistence.Entities;

namespace SweatSpace.Api.Persistence
{
    public static class DataSeed
    {
        public static async Task SeedUsers(UserManager<AppUser> userManager, RoleManager<AppRole> roleManager)
        {
            if (userManager.Users.Any())
            {
                return;
            }

            var userData = await File.ReadAllTextAsync("Persistence/UserData.json");
            var users = JsonSerializer.Deserialize<List<AppUser>>(userData);
            var roles = new List<AppRole>
            {
                new AppRole {Name = "Admin"},
                new AppRole {Name = "Member"}
            };

            foreach (var role in roles)
            {
                await roleManager.CreateAsync(role);
            }

            foreach (var user in users)
            {
                //creates and saves the user in Db
                await userManager.CreateAsync(user, "Password123.");
                await userManager.AddToRoleAsync(user, "Member");
            }

            var adminUser = new AppUser { UserName = "admin" };
            await userManager.CreateAsync(adminUser, "Password123.");
            await userManager.AddToRoleAsync(adminUser, "Admin");
        }
    }
}