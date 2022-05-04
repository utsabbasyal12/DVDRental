namespace DVDRental.Models
{
    public class CastMember
    {
        public int ActorId { get; set; }
        public Actor? Actor { get; set; }
        
        public int DVDNumber { get; set; }
        public DVDTitle? DVDTitle { get; set; }

    
    }
}
