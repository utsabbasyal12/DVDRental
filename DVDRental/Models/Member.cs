namespace DVDRental.Models
{
    public class Member
    {
        public int MemberNumber { get; set; }
        public string? MemberLastName { get; set; }
        public string? MemberFirstName { get; set; }
        public string? MemberAddress { get; set; }
        public string? MemberDateOfBirth { get; set; }
        public virtual ICollection<MembershipCategory>? MembershipCategory { get; set; }
    }
}
