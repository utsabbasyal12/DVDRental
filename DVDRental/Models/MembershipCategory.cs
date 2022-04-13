namespace DVDRental.Models
{
    public class MembershipCategory
    {
        public int MembershipCategoryNumber { get; set; }   
        public string? MembershipCategoryDescription { get; set; }
        public int MembershipCategoryTotalLoans { get; set; }
    }
}
