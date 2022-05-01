using System.ComponentModel.DataAnnotations;

namespace DVDRental.Models
{
    public class Actor
    {
        [Key]
        public int ActorId { get; set; }
        public string? ActorSurname { get; set; }
        public string? ActorFirstName { get; set; }

        //Relationships
        public List<CastMember>? CastMembers { get; set; }


    }
}
