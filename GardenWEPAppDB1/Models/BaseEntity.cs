using Microsoft.AspNetCore.Identity;

namespace GardenWEPAppDB1.Models
{
    public class BaseEntity
    {
        public int Id { get; set; }
        public bool IsActive { get; set; }
        public bool IsDelete { get; set; }
        public DateTime CreateDate { get; set; } = DateTime.Now;
        public DateTime UpdateDate { get; set; } 
        public DateTime DeleteDate { get; set; }
    }
}
