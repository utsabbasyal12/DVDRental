namespace DVDRental.Models.ViewModels
{
    public class UnloanedMembersVM
    {
        public string? MemberFirstName { get; set; }
        public string? MemberLastName { get; set; }
        public string? Address { get; set; }
        public string? DVDTitle { get; set; }
        public DateTime? DateOut { get; set; }
        public double? LoanedDays { get; set; }

    }
}
