using DVDRental.Enums;
using System.ComponentModel.DataAnnotations;

namespace DVDRental.Models
{
    public class DVDCategory
    {
        [Key]
        public int CategoryNumber { get; set; }
        public CategoryDescription CategoryDescription { get; set; }
        public AgeRestricted AgeRestricted { get; set; }

        //Relationships
        public List<DVDTitle>? DVDTitles { get; set; }


    }
}
