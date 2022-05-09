using DVDRental.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace DVDRental.Controllers
{
    
    public class Authentication : Controller
    {
        private readonly UserManager<ApplicationUser> userManager;

        public Authentication(UserManager<ApplicationUser> userManager)
        {
            this.userManager = userManager; 
        }
        public IActionResult Index()
        {
            var users = userManager.Users.ToList();
            return View(users);
        }
    }
}
