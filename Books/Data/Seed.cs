using Books.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Text.Json;
using System.Threading.Tasks;

namespace Books.Data
{
    public class Seed
    {
        public static async Task SeedUsers(UserManager<AppUser> customerManager,
            RoleManager<AppRole> roleManager)
        {
            if (await customerManager.Users.AnyAsync()) return;

            var userData = await System.IO.File.ReadAllTextAsync("Data/UserSeedData.json");
            var users = JsonSerializer.Deserialize<List<AppUser>>(userData);
            if (users == null) return;

            var roles = new List<AppRole>
            {
                new AppRole{Name= "Member"},
                new AppRole{Name= "Admin"},
                new AppRole{Name= "Moderator"},
            };

            foreach (var role in roles)
            {
                await roleManager.CreateAsync(role);
            }

            foreach (var user in users)
            {
               

                user.UserName = user.UserName.ToLower();
                

                await customerManager.CreateAsync(user, "Kolikoje3");
                await customerManager.AddToRoleAsync(user, "Member");
            }

            var admin = new AppUser
            {
                UserName = "admin"
            };

            await customerManager.CreateAsync(admin, "Kolikoje3");
            await customerManager.AddToRolesAsync(admin, new[] { "Admin", "Moderator" });

        }
    }
}
