using GardenWEPAppDB1.Models;

namespace GardenWEPAppDB1.ViewModelVM
{
    public class RoelModelVM
    {
        public User User { get; set; }
        public IEnumerable<string> Roles { get; set; }
    }
}
