namespace DVDRental.Models.ViewModels
{
    public class NewDVDDropdownsVM
    {
        public NewDVDDropdownsVM()
        {
            Producers = new List<Producer>();
            Studios = new List<Studio>();
            Actors = new List<Actor>();
        }
        public List<Producer> Producers { get; set; }
        public List<Studio> Studios{ get; set; }
        public List<Actor> Actors { get; set; }
    }
}