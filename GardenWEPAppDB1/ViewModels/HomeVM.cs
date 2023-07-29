using GardenWEPAppDB1.Models;

namespace GardenWEPAppDB1.ViewModels
{
    public class HomeVM
    {
        public List<Article> Articles { get; set; }
        public List <Article> PopularProduct { get; set; }
        public List<Article> FirstSlot { get; set; }
        public List<Article> Allslot { get; set; }
        public List<User> User { get; set; }
    }
}
