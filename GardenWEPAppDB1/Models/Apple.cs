using System.ComponentModel.DataAnnotations;

namespace GardenWEPAppDB1.Models
{
    public class Apple : BaseEntity
    {
        [Required]
        public string Title { get; set; }
        [Required]
        public string Context { get; set; }
        public int ViewCount { get; set; }
        public User User { get; set; }
        public string UserId { get; set; }
        public List<ArticleTag> ArticleTag { get; set; }
        public Category Category { get; set; }
        public string PhotoUrl { get; set; }
        public int CategoryId { get; set; }
    }


}

