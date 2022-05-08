using DVDRental.Models;
using DVDRental.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace DVDRental.Controllers
{
    [Authorize(Roles = "Manager")]
    public class UserManagement : Controller
    {
        private readonly UserManager<ApplicationUser> userManager;

        public UserManagement(UserManager<ApplicationUser> userManager)
        {
            this.userManager = userManager; 
        }
        public IActionResult UserDetails()
        {
            var users = userManager.Users.ToList();
            return View(users);
        }

        [HttpGet]
        public async Task<IActionResult> EditUser(string id)
        {
            var user = await userManager.FindByIdAsync(id);

            if (user == null)
            {
                ViewBag.ErrorMessage = "$User with Id = {id} cannot be found";
                return View("Not found");
            }

            // GetClaimsAsync retunrs the list of user Claims
            var userClaims = await userManager.GetClaimsAsync(user);
            // GetRolesAsync returns the list of user Roles
            var userRoles = await userManager.GetRolesAsync(user);

            var model = new EditUserViewModel
            {
                Id = user.Id,
                Email = user.Email,
                UserName = user.UserName,
                Claims = userClaims.Select(c => c.Value).ToList(),
                Roles = userRoles
            };

            return View(model);
        }
    }
}
