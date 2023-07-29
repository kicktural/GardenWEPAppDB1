using GardenWEPAppDB1.Models;

namespace GardenWEPAppDB1.ViewModAPPle
{
    public class DetailVMApple
    {
        public Apple Apple { get; set; }
        public List<Apple> PopularApple { get; set; }
        public List<Apple> NewProduct { get; set; }
    }
}
