using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PetShopTaskMVC.DataContext;
using PetShopTaskMVC.Models;

namespace PetShopTaskMVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly AppDbContext _dbContext;

        public HomeController(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public IActionResult Index()
        {
            var products = _dbContext.Products.Include(c=>c.Category).Include(pt => pt.ProductTags).ThenInclude(t=>t.Tag).ToList();
            var categories = _dbContext.Categories.ToList();
            var petClothes = _dbContext.Products.Where(p => p.ProductTags.Any(pt => pt.TagId == 2)).ToList();
            var petFoods = _dbContext.Products.Where(p => p.ProductTags.Any(pt => pt.TagId == 3)).ToList();
            var homeViewModel = new HomeViewModel()
            {
                Products = products,
                Categories=categories,
                PetClothes=petClothes,
                PetFoods=petFoods
            };

            return View(homeViewModel);
        }

    }
}
