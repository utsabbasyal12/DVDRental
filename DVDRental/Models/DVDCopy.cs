namespace DVDRental.Models
{
    public class DVDCopy
    {
        public int CopyNumber { get; set; } 
        public DateTime DatePurchased { get; set; }
        public DVDTitle?  DVDTitle { get; set; }  
    }
}
  