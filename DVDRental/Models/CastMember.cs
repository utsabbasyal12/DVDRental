namespace DVDRental.Models
{
    public class CastMember
    {
        public int Id { get; set; }
        public int ActorId { get; set; }
        public Actor? Actor { get; set; }
        
        public int DVDNumber { get; set; }
        public DVDTitle DVDTitle { get; set; }

       /* public virtual ICollection<DVDTitle>? DVDTitle { get; set; }
        public virtual ICollection<Actor>? Actor { get; set; }*/

    }
}
