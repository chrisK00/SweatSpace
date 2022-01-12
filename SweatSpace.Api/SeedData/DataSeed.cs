using Microsoft.AspNetCore.Identity;
using SweatSpace.Core.Entities;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace SweatSpace.Api.SeedData
{
    public static class DataSeed
    {
        public static async Task SeedUsers(UserManager<AppUser> userManager, RoleManager<AppRole> roleManager)
        {
            if (userManager.Users.Any())
            {
                return;
            }

            var userData = await File.ReadAllTextAsync("SeedData/UserData.json");
            var users = JsonSerializer.Deserialize<List<AppUser>>(userData);

            var adminRole = new AppRole { Name = "Admin" };
            await roleManager.CreateAsync(adminRole);

            foreach (var user in users)
            {
                //creates and saves the user in Db
                await userManager.CreateAsync(user, "Password123.");
            }

            var adminUser = new AppUser { UserName = "admin" };
            await userManager.CreateAsync(adminUser, "Password123.");
            await userManager.AddToRoleAsync(adminUser, adminRole.Name);
        }
    }
}