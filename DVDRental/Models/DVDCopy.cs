namespace DVDRental.Models
{
    public class DVDCopy
    {
        public int CopyNumber { get; set; } 
        public string? DatePurchased { get; set; }
        public virtual DVDTitle?  DVDTitle { get; set; }  
    }
}
  