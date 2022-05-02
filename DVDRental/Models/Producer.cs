using System.ComponentModel.DataAnnotations;

namespace DVDRental.Models
{
    public class Producer
    {
        [Key]
        public int ProducerNumber { get; set; }

        [Display(Name = "Producer Name")]
        [Required(ErrorMessage = "Producer name is required")]
        public string? ProducerName { get; set; } 

        //Relationships
        public List<DVDTitle>? DVDTitles { get; set; }

    }
}
