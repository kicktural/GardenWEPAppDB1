namespace GardenWEPAppDB1.Models
{
    public class Category : BaseEntity
    {
        public string CategoryName { get; set; }
        public List<Article>? article { get; set; }
        public List<Apple>? apple { get; set; }
    }
}
