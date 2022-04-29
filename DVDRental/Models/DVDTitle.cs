using System.ComponentModel.DataAnnotations;

namespace DVDRental.Models
{
    public class DVDTitle
    {
        [Key]
        public int DVDNumber { get; set; }
        public string? Title { get; set; }
        public DateTime DateRelease { get; set; }    
        public decimal StandardCharge { get; set; }
        public decimal PenaltyCharge { get; set; }
        public DVDCategory? DVDCategory{ get; set; }
        public Studio? Studio { get; set; }
        public Producer? Producer { get; set; }
        public List<CastMember>? CastMembers { get; set; } 
    }
}
