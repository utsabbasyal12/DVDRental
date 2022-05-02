using DVDRental.Areas.Identity.Data;
using DVDRental.Data.Base;
using DVDRental.Data.Interface;
using DVDRental.Models;
using DVDRental.Models.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace DVDRental.Data.Services
{
    public class DVDTitleService : EntityBaseRepository<DVDTitle>, IDVDTitleService
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
                StudioId = data.StudioId,
                DVDCategory = data.DVDCategory,
                ProducerNumber = data.ProducerNumber
            };
            await _context.DVDTitles.AddAsync(newDVD);
            await _context.SaveChangesAsync();

            //Add Movie Actors
            foreach (var actorId in data.ActorId)
            {
                var newActorDVD = new CastMember()
                {
                    DVDNumber = newDVD.DVDNumber,
                    ActorId = actorId
                };
                await _context.CastMembers.AddAsync(newActorDVD);
            }
            await _context.SaveChangesAsync();
        }

        public async Task<DVDTitle> GetDVDTitleByIdAsync(int id)
        {
            var dvdDetails = await _context.DVDTitles
                .Include(c => c.Studios)
                .Include(p => p.Producers)
                .Include(am => am.CastMembers).ThenInclude(a => a.Actor)
                .FirstOrDefaultAsync(n => n.DVDNumber== id);

            return dvdDetails;
        }

        public async Task<NewDVDDropdownsVM> GetNewMovieDropdownsValues()
        {
            var response = new NewDVDDropdownsVM()
            {
                Actors = await _context.Actors.OrderBy(n => n.ActorFirstName).ToListAsync(),
                Studios = await _context.Studios.OrderBy(n => n.StudioName).ToListAsync(),
                Producers = await _context.Producers.OrderBy(n => n.ProducerName).ToListAsync()
            };

            return response;
        }

        public async Task UpdateMovieAsync(NewDVDVM data)
        {
            var dbDVD = await _context.DVDTitles.FirstOrDefaultAsync(n => n.DVDNumber == data.Id);

            if (dbDVD != null)
            {
                dbDVD.Title = data.Title;
                dbDVD.DateRelease = data.DateRelease;
                dbDVD.StudioId = data.StudioId;
                dbDVD.DVDCategory = data.DVDCategory;
                dbDVD.ProducerNumber = data.ProducerNumber;
                await _context.SaveChangesAsync();
            }

            //Remove existing actors
            var existingActorsDb = _context.CastMembers.Where(n => n.DVDNumber == data.Id).ToList();
            _context.CastMembers.RemoveRange(existingActorsDb);
            await _context.SaveChangesAsync();

            //Add Movie Actors
            foreach (var actorId in data.ActorId)
            {
                var newActorMovie = new CastMember()
                {
                    DVDNumber = data.Id,
                    ActorId = actorId
                };
                await _context.CastMembers.AddAsync(newActorMovie);
            }
            await _context.SaveChangesAsync();
        }
    }
}
