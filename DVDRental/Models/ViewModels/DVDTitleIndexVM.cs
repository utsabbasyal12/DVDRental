namespace DVDRental.Models.ViewModels
{
    public class DVDTitleIndexVM
    {
        public DVDTitle? DVDTitle { get; set; }  
        public Actor? Actor { get; set; }   
        public Producer? Producer { get; set; } 
        public Studio? Studio { get; set; } 
        public DVDCategory? DVDCategory { get; set; }
        public CastMember? CastMember { get; set; }
    }
}
