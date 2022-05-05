namespace DVDRental.Models.ViewModels
{
    public class DVDTitleListCastVM
    {
        public string? DVDTitle { get; set; }
        public DateTime? DateReleased { get; set; }
        public string? ActorFirstName { get; set; }   
        public string? ActorLastName { get; set; }   
        public string? Producer { get; set; } 
        public string? Studio { get; set; } 
    }
}
