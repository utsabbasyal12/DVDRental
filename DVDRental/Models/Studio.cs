using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace DVDRental.Models
{
    public class Studio
    {
        [Key]
        public int StudioId { get; set; }

        [Display(Name = "Studio Name")]
        [Required(ErrorMessage = "Studio name is required")]
        public string? StudioName { get; set; }

        //Relationships
        public List<DVDTitle>? DVDTitles { get; set; }

    }
}
