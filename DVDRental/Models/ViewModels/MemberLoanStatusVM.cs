namespace DVDRental.Models.ViewModels
{
    public class MemberLoanStatusVM
    {
        public int? MemberNumber { get; set; }
        public string? MemberFirstName { get; set; }
        public string? MemberLastName { get; set; }
        public string? MemberAddress { get; set; }
        public DateTime? MemberDOB { get; set; }
        public DateTime? DateReturned { get; set; }
        public string? MembershipCategory { get; set; }
        public int? CategoryTotalLoans { get; set; }
        public int? MemberLoanCount { get; set; }
        public string? Remark { get; set; }

    }
}
