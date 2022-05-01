using DVDRental.Enums;
using System.ComponentModel.DataAnnotations;

namespace DVDRental.Models
{
    public class MembershipCategory
    {
        [Key]
        public int MembershipCategoryNumber { get; set; }   
        public MembershipCategoryDescription MembershipCategoryDescription { get; set; }
        public int MembershipCategoryTotalLoans { get; set; }

        //Relationships
        public List<Member>? Members{ get; set; }

    }
}
