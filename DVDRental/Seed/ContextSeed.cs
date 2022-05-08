using DVDRental.Areas.Identity.Data;
using DVDRental.Models;
using Microsoft.AspNetCore.Identity;

namespace DVDRental.Seed
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

        /* private static async Task CreateRoles(AppDBContext context, IServiceProvider serviceProvider)
         {
             var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();
             var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
             // First, Creating User role as each role in User Manager  
             List<IdentityRole> roles = new List<IdentityRole>();
             roles.Add(new IdentityRole { Name = "Manager", NormalizedName = "ADMINISTRATOR" });
             roles.Add(new IdentityRole { Name = "Staff", NormalizedName = "MEMBER" });

             //Then, the machine added Default User as the Admin user role
             foreach (var role in roles)
             {
                 var roleExit = await roleManager.RoleExistsAsync(role.Name);
                 if (!roleExit)
                 {
                     context.Roles.Add(role);
                     context.SaveChanges();
                 }
             }
             //Next, I create an Admin Super User who will maintain the LMS website panel
             var userAdmin = new ApplicationUser()
             {
                 UserName = "Utsav",
                 Email = "manager@gmail.com",
             };
             string userPWD = "Manager@123";
             var chkUser = userManager.AddPasswordAsync(userAdmin, userPWD);
             if (chkUser.IsFaulted)
             {
                 await roleManager.CreateAsync(new IdentityRole { Name = "Manager", NormalizedName = "ADMINISTRATOR" });
             }
             userAdmin = await userManager.FindByNameAsync("Utsav");
             await userManager.AddToRoleAsync(userAdmin, "Manager");
             //await userManager.AddToRoleAsync(user, "Admin");
         }*/
    }
}
