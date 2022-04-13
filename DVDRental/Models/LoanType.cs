namespace DVDRental.Models
{
    public class LoanType
    {
        public int LoanTypeNumber { get; set; }
        public IEnumerable<string>? LoanCategory { get; set; }
        public int LoanDuration { get; set; }   


    }
}
