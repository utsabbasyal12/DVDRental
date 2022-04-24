using System.ComponentModel.DataAnnotations;

namespace DVDRental.Models
{
    public class Producer
    {
        [Key]
        public int ProducerNumber { get; set; }
        public string? ProducerName { get; set; } 

    }
}
