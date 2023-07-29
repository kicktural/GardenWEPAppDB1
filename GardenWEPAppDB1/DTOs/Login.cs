using System.ComponentModel.DataAnnotations;

namespace GardenWEPAppDB1.DTOs
{
	public class Login
	{
        public string Email { get; set; }
        public string Password { get; set; }
        public bool RememBerme { get; set; }
    }
}
