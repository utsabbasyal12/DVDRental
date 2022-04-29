using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DVDRental.Models
{
    [NotMapped]
    public class LoanType
    {
        [Key]
        public int LoanTypeNumber { get; set; }
        public IEnumerable<string>? LoanCategory { get; set; }
        public int LoanDuration { get; set; }   


    }
}
