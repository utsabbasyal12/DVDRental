using DVDRental.Areas.Identity.Data;
using DVDRental.Models;

namespace DVDRental.Data.Services
{
    public interface IUserService
    {
        ApplicationUser Get(string id);
        Task<ApplicationUser> Update(ApplicationUser dVDRentalUser);
    }
}