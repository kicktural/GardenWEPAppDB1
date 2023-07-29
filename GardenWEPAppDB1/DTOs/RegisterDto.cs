using Microsoft.Build.Framework;
using System.ComponentModel.DataAnnotations;

namespace GardenWEPAppDB1.DTOs
{
	public class RegisterDto
	{
   
        public string FisrtName { get; set; }
        public string LastName { get; set; }
        [EmailAddress]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
     
        public string Password { get; set; }
      
        [Compare("Password")]
        public string PasswordConfirmation { get; set; }
    }
}
