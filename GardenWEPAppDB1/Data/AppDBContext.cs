using GardenWEPAppDB1.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace GardenWEPAppDB1.Data
{
    public class AppDBContext : IdentityDbContext<User>
    {
      
     public AppDBContext(DbContextOptions options) :base(options) 
        {
        }

        public DbSet<Article> ArticlesDB { get; set; }
        public DbSet<ArticleTag> articleTagsDB { get; set; }
        public DbSet<Tag> TagsDB { get; set; }
        public DbSet<Category> CategoriesDB { get; set; }
        public DbSet<Apple> AppleDBs { get; set; }
        public DbSet<Reklam> ReklamDBs { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<IdentityRole> IdentityRoles { get; set; }

        //protected override void OnModelCreating(ModelBuilder builder)
        //{
        //    base.OnModelCreating(builder);
        //    builder.Entity<User>().ToTable("UsersDB");
        //    builder.Entity<IdentityRole>().ToTable("RolesDB");
        //}
    }
}
