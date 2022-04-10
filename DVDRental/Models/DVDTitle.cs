namespace DVDRental.Models
{
    public class DVDTitle
    {
        public int DVDNumber { get; set; }
        public string? Title { get; set; }
        public string? DateRelease { get; set; }    
        public string? StandardCharge { get; set; }
        public string? PenaltyCharge { get; set; }
        public virtual DVDCategory? DVDCategory{ get; set; }
        public virtual Studio? Studio { get; set; }
        public virtual Producer? Producer { get; set; }
    }
}
