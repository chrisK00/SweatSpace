using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SweatSpace.Api.Persistence.Entities;
using SweatSpace.Api.Persistence.Interfaces;
using SweatSpace.Persistence.Business;

namespace SweatSpace.Api.Persistence
{
    public static class DataSeed
    {
        public static async Task SeedUsers(UserManager<AppUser> userManager)
        {
            if (userManager.Users.Any())
            {
                return;
            }

            var userData = await File.ReadAllTextAsync("Persistence/UserData.json");
            var users = JsonSerializer.Deserialize<List<AppUser>>(userData);

            foreach (var user in users)
            {
                //creates and saves the user in Db
                await userManager.CreateAsync(user, "Password123.");
            }         
        }
    }
}
