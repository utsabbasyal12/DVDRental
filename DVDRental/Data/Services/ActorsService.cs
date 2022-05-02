using DVDRental.Areas.Identity.Data;
using DVDRental.Data.Base;
using DVDRental.Data.Interface;
using DVDRental.Models;

namespace DVDRental.Data.Services
{
    public class ActorsService : EntityBaseRepository<Actor>, IActorsService
    {
        public ActorsService(AppDBContext context) :base(context)
        {
        
        }

    }
}
