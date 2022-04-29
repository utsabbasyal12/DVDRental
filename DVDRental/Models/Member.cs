using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DVDRental.Models
{
    [NotMapped]
    public class Member
    {
        [Key]
        public int MemberNumber { get; set; }
        public string? MemberLastName { get; set; }
        public string? MemberFirstName { get; set; }
        public string? MemberAddress { get; set; }
        public DateOnly MemberDOB { get; set; }
        public MembershipCategory? MembershipCategory { get; set; }
    }
}
