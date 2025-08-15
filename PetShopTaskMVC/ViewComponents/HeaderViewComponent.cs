using Microsoft.AspNetCore.Mvc;
using PetShopTaskMVC.DataContext;
using PetShopTaskMVC.Models;

namespace PetShopTaskMVC.ViewComponents
{
    public class HeaderViewComponent:ViewComponent
    {
        private readonly AppDbContext _dbContext;
        
        public HeaderViewComponent(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var bio = _dbContext.Bios.FirstOrDefault();

            var headerViewModel = new HeaderViewModel()
            {
                Bio = bio
            };

            return View(headerViewModel);
        }
    }
}
