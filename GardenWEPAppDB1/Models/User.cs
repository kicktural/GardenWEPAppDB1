using Microsoft.AspNetCore.Identity;

namespace GardenWEPAppDB1.Models
{
    public class User : IdentityUser
    {
        public string FistName { get; set; }
        public string LastName { get; set; }
        public string PhotoUrl { get; set; }
        public string? AboutAuthor { get; set; }
    }
}
