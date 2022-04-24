using System.ComponentModel.DataAnnotations;

namespace DVDRental.Models
{
    public class Loan
    {
        [Key]
        public int LoanNumber { get; set; }
        public LoanType? LoanType { get; set; }
        public DVDCopy? DVDCopy { get; set; }
        public Member? Member { get; set; }
        public DateTime DateOut { get; set; }
        public DateTime DateDue { get; set; }
        public DateTime DateRetured { get; set; }

    }
}
