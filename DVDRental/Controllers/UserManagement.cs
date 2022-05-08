using DVDRental.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace DVDRental.Controllers
{
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
    }
}
