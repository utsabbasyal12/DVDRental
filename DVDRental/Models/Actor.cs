
using System.ComponentModel.DataAnnotations;

namespace DVDRental.Models
{
    public class Actor
    {
        [Key]
        public int ActorId { get; set; }
        [Display(Name = "Last Name")]
        [Required(ErrorMessage = "Last Name is required")]
        public string? ActorSurname { get; set; }
        [Display(Name = "First Name")]
        [Required(ErrorMessage = "First Name is required")]
        public string? ActorFirstName { get; set; }

        //Relationships
        public List<CastMember>? CastMembers { get; set; }


    }
}
