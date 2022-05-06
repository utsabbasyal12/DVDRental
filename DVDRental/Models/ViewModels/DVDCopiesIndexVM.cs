namespace DVDRental.Models.ViewModels
{
    public class DVDCopiesIndexVM
    {
        public string? MemberFirstName { get; set; }
        public string? MemberLastName { get; set; }
        public DateTime? DateOut { get; set; }   
        public DateTime? DateDue { get; set; } 
        public DateTime? DateReturned { get; set; } 
        public string? DVDTitle { get; set; } 
        public int? CopyNumber { get; set; }
    }
}
