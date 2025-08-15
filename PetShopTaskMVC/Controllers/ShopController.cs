using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PetShopTaskMVC.DataContext;
using PetShopTaskMVC.Models;

namespace PetShopTaskMVC.Controllers
{
    public class ShopController:Controller
    {
        private readonly AppDbContext _dbContext;

        public ShopController(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        
        public IActionResult Index()
        {
            var products = _dbContext.Products.Include(x=>x.Category).Include(pt=>pt.ProductTags)
                .ThenInclude(t=>t.Tag).Take(3).ToList();
            var categories = _dbContext.Categories.ToList();
            var tags = _dbContext.Tags.ToList();

            ViewBag.ProductCount = _dbContext.Products.Count();

            var shopViewModel = new ShopViewModel()
            {
                Products = products,
                Categories = categories,
                Tags = tags
            };

            return View(shopViewModel);
        }

        public IActionResult Partial (int skip)
        {
            var products = _dbContext.Products.Include(x => x.Category).Include(pt => pt.ProductTags)
                .ThenInclude(t => t.Tag).Skip(skip).Take(3).ToList();

            return PartialView("_ProductPartialLoadMore", products);
        }
    }
}
