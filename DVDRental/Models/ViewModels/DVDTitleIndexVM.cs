namespace DVDRental.Models.ViewModels
{
    public class DVDTitleIndexVM
    {
        public string? DVDTitle { get; set; }
        public int? DVDNumber { get; set; }
        public string? Actor { get; set; }   
        public string? Producer { get; set; } 
        public string? Studio { get; set; } 
        public string? DVDCategory { get; set; }
        public string? CastMember { get; set; }
        public DateTime? DateReleased { get; set; }
        public decimal? StandardCharge { get; set; }
        public decimal? PenaltyCharge { get; set; }
    }
}
