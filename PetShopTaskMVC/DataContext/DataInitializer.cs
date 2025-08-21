using Microsoft.EntityFrameworkCore;
using PetShopTaskMVC.DataContext.Entities;
namespace PetShopTaskMVC.DataContext
{
    public class DataInitializer
    {
        private readonly AppDbContext _dbContext;

        public DataInitializer(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void SeedData()
        {
            _dbContext.Database.Migrate();

            //var logo = new Bio { LogoUrl = "logo.png" };
            //_dbContext.Bios.Add(logo);
            //_dbContext.SaveChanges();
        }
    }
}
