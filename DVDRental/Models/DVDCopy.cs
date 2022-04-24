using System.ComponentModel.DataAnnotations;

namespace DVDRental.Models
{
    public class DVDCopy
    {
        [Key]
        public int CopyNumber { get; set; } 
        public DateTime DatePurchased { get; set; }
        public DVDTitle?  DVDTitle { get; set; }  
    }
}
  