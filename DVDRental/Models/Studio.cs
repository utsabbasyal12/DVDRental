using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace DVDRental.Models
{
    public class Studio
    {
        [Key]
        public int StudioId { get; set; }
        public string? StudioName { get; set; }

        //Relationships
        public List<DVDTitle>? DVDTitles { get; set; }

    }
}
