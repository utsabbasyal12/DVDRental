using DVDRental.Models;
using Microsoft.AspNetCore.Identity;

namespace DVDRental.Data
{
    public static class ContextSeed
    {
        public static async Task SeedRolesAsync(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            //Seed Roles
            await roleManager.CreateAsync(new IdentityRole(Enums.Roles.Manager.ToString()));
            await roleManager.CreateAsync(new IdentityRole(Enums.Roles.Staff.ToString()));
        }
        public static async Task SeedManagerAsync(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            //Seed Default User
            var defaultUser = new ApplicationUser
            {
                ShopName = "Ropey DVD",
                UserName = "manager",
                Email = "manager@gmail.com",
                EmailConfirmed = true, 
                PhoneNumberConfirmed = true 
            };
            if (userManager.Users.All(u => u.Id != defaultUser.Id))
            {
                var user = await userManager.FindByEmailAsync(defaultUser.Email);
                if (user == null)
                {
                    await userManager.CreateAsync(defaultUser, "Manager@123");
                    await userManager.AddToRoleAsync(defaultUser, Enums.Roles.Manager.ToString());
                    await userManager.AddToRoleAsync(defaultUser, Enums.Roles.Staff.ToString());
                }
            }
        }
    }
}
