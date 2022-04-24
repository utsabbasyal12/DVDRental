using System.ComponentModel.DataAnnotations;

namespace DVDRental.Models
{
    public class DVDCategory
    {
        [Key]
        public int CategoryNumber { get; set; }
        public string? CategoryDescription { get; set; }
        public bool? AgeRestricted { get; set; }

    }
}
