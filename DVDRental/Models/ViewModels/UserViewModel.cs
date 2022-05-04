using DVDRental.Areas.Identity.Data;
using PagedList;

namespace DVDRental.Models.ViewModels
{
    public class UserViewModel
    {
        public DVDRentalUser? User { get; set; }
        public IPagedList<Actor>? Actors { get; set; }
        public int SearchString { get; set; }

    }
}

