using DVDRental.Areas.Identity.Data;
using DVDRental.Models;
using DVDRental.Models.ViewModels;

namespace DVDRental.Data.Services
{
    public class DVDTitleService : IDVDTitleService
    { 
        private readonly AppDBContext _context;
        public DVDTitleService(AppDBContext context) 
        {
            _context = context;
        }

        public async Task AddNewDVDAsync(NewDVDVM data)
        {
            var newDVD = new DVDTitle()
            {
                Title = data.Title,
                DateRelease = data.DateRelease,
                StandardCharge= data.StandardCharge,
                PenaltyCharge = data.PenaltyCharge,
                StudioId = data.StudioId,
                DVDCategory = data.DVDCategory,
                ProducerNumber = data.ProducerNumber
            };
            await _context.DVDTitles.AddAsync(newDVD);
            await _context.SaveChangesAsync();

            //Add Movie Actors
            foreach (var actorId in data.ActorId)
            {
                var newCastMember= new CastMember()
                {
                    DVDNumber = newDVD.DVDNumber,
                    ActorId = actorId
                };
                await _context.CastMembers.AddAsync(newCastMember);
            }
            await _context.SaveChangesAsync();
        }
    }
}
