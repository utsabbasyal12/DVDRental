namespace DVDRental.Models.ViewModels
{
    public class DVDTitleCreateVM
    {
        public string Title { get; set; }
        public DateTime DateReleased { get; set; }
        public decimal StandardCharge { get; set; }
        public decimal PenaltyCharge { get; set; }
        public string StudioName { get; set; }
        public string ProducerName { get; set; }
        public int  CategoryNumber { get; set; }
        

    }
}
