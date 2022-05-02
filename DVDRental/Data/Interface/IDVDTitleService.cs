using DVDRental.Data.Base;
using DVDRental.Models;
using DVDRental.Models.ViewModels;

namespace DVDRental.Data.Interface
{
    public interface IDVDTitleService : IEntityBaseRepository<DVDTitle>
    {
        Task AddNewDVDAsync(NewDVDVM data);
        Task<DVDTitle> GetDVDTitleByIdAsync(int id);
        Task<NewDVDDropdownsVM> GetNewMovieDropdownsValues();
        Task UpdateMovieAsync(NewDVDVM data);
    }
}
