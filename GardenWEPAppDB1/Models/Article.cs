using System.ComponentModel.DataAnnotations;

namespace GardenWEPAppDB1.Models
{
    public class Article : BaseEntity
    {
        [Required]
        [MinLength(5)]
        [MaxLength(35)]
        public string Title { get; set; }
        [Required]
        [MinLength(15)]
        [MaxLength(300)]
        public string Context { get; set; }
        public int ViewCount { get; set; }
        public string UserId { get; set; }
        public User User { get; set; }
        public string SeoUrl { get; set; }
        [Required(ErrorMessage = "Won't send null!")]
        public string PhotoUrl { get; set; }
        public int CategoryId { get; set; }
        [Required]
        public List<ArticleTag> Articletag { get; set; }
        public Category Category { get; set; }
    }
}
