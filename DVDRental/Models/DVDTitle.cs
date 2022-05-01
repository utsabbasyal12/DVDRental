using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DVDRental.Models
{
    [NotMapped]
    public class DVDTitle
    {
        [Key]
        public int DVDNumber { get; set; }
        public string? Title { get; set; }
        public DateTime DateRelease { get; set; }    
        public decimal StandardCharge { get; set; }
        public decimal PenaltyCharge { get; set; }

        //Relationships
        public List<CastMember>? CastMembers { get; set; } 
        public List<DVDCopy>? DVDCopys { get; set; }

        //Studio
        public int StudioId { get; set; }
        [ForeignKey("StudioId")]
        public Studio? Studios { get; set; }

        //Producer
        public int ProducerNumber { get; set; }
        [ForeignKey("ProducerNumber")]
        public Producer? Producers { get; set; }
        
        //DVDCategory
        public int CategoryNumber { get; set; }
        [ForeignKey("CategoryNumber")]
        public DVDCategory? DVDCategory{ get; set; }

        
    }
}
