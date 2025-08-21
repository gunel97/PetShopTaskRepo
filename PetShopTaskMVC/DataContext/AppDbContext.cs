using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PetShopTaskMVC.DataContext.Entities;

namespace PetShopTaskMVC.DataContext
{
    public class AppDbContext:IdentityDbContext<AppUser>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
                   
        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<ProductImage> ProductImages {  get; set; }
        public DbSet<ProductTag> ProductTags { get; set; }
        public DbSet<Slider> Sliders { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<Social> Socials { get; set; }
        public DbSet<Bio> Bios { get; set; }

    }
}
