using Microsoft.AspNetCore.Mvc;

namespace DVDRental.Controllers
{
    public class Authentication : Controller
    {
        public IActionResult Index()
        {

            return View();
        }
    }
}
