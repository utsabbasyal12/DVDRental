using DVDRental.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DVDRental.Models
{
    public class LoanType
    {
        [Key]
        public int LoanTypeNumber { get; set; }
        public LoanCategory LoanCategory { get; set; }
        public int LoanDuration { get; set; }

        //Relationships
        public List<Loan>? Loan{ get; set; }



    }
}
