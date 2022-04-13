namespace DVDRental.Models
{
    public class CastMember
    {
        public virtual ICollection<DVDTitle>? DVDTitle { get; set; }
        public virtual ICollection<Actor>? Actor { get; set; }

    }
}
