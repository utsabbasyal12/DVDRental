using System.ComponentModel.DataAnnotations;

namespace DVDRental.Models.ViewModels
{
    public class NewDVDVM
    {
        public int Id { get; set; }

        [Display(Name = "DVD name")]
        [Required(ErrorMessage = "Name is required")]
        public string? Title { get; set; }

        [Display(Name = "DVD DateRelease")]
        [Required(ErrorMessage = "Release date is required")]
        public DateTime DateRelease { get; set; }

        public decimal StandardCharge { get; set; }

        public decimal PenaltyCharge { get; set; }

        [Display(Name = "Select a category")]
        [Required(ErrorMessage = "DVD category is required")]
        public DVDCategory? DVDCategory{ get; set; }

        //Relationships
        [Display(Name = "Select actor(s)")]
        [Required(ErrorMessage = "Movie actor(s) is required")]
        public List<int>? ActorId { get; set; }

        [Display(Name = "Select a studio")]
        [Required(ErrorMessage = "Movie studio is required")]
        public int StudioId { get; set; }

        [Display(Name = "Select a producer")]
        [Required(ErrorMessage = "Movie producer is required")]
        public int ProducerNumber { get; set; }
    }
}
