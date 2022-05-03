using DVDRental.Areas.Identity.Data;
using DVDRental.Data.Interface;
using DVDRental.Models;

namespace DVDRental.Data.Services
{
    public class StudiosService : IStudiosService
    {
        public StudiosService(AppDBContext context)
        {
        }
    }
}
