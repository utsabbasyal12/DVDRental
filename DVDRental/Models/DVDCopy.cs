using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DVDRental.Models
{
    public class DVDCopy
    {
        [Key]
        public int CopyNumber { get; set; } 
        public DateTime DatePurchased { get; set; }

        //Relationships
        public List<Loan>? Loan { get; set; }

        //DVDTitles
        public int DVDNumber { get; set; }
        [ForeignKey("DVDNumber")]
        public DVDTitle?  DVDTitle { get; set; }
    }
}
  