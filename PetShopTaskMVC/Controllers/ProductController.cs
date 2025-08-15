using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PetShopTaskMVC.DataContext;
using PetShopTaskMVC.Models;

namespace PetShopTaskMVC.Controllers
{
    public class ProductController : Controller
    {
        private readonly AppDbContext _dbContext;

        public ProductController(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IActionResult Details(int id)
        {
            var product = _dbContext.Products.Include(c => c.Category).Include(i => i.Images)
                .Include(pt => pt.ProductTags).ThenInclude(t => t.Tag).FirstOrDefault(x => x.Id == id);

            if (product == null)
                return NotFound();

            var productViewModel = new ProductViewModel()
            {
                Product = product,
            };

            return View(productViewModel);
        }
    }
}
