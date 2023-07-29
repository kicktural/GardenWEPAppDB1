using GardenWEPAppDB1.Models;

namespace GardenWEPAppDB1.ViewModels
{
    public class DetailVM
    {
        public Article articles { get; set; }
        public List<Article> PopularArticle { get; set; }
        public Article NextArticle { get; set; }
        public Article PrewArticle { get; set; }
        public IEnumerable<Article> SimilarArticle { get; set; }
        public List <Article> Articles11 { get; set; }
    }
}
