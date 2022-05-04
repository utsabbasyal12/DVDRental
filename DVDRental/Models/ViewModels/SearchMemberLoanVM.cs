namespace DVDRental.Models.ViewModels
{
    public class SearchMemberLoanVM
    {
        public string? DVDTitle { get; set; }
        public string? MemberFirstName { get; set; }
        public string? MemberLastName { get; set; }
        public int? CopyNumber { get; set; }

    }
}
