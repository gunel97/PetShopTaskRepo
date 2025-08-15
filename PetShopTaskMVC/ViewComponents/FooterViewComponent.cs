using Microsoft.AspNetCore.Mvc;
using PetShopTaskMVC.DataContext;
using PetShopTaskMVC.Models;

namespace PetShopTaskMVC.ViewComponents
{
    public class FooterViewComponent:ViewComponent
    {
        private readonly AppDbContext _dbContext;

        public FooterViewComponent(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var bio = _dbContext.Bios.FirstOrDefault();
            var socials = _dbContext.Socials.ToList();

            var footerViewModel = new FooterViewModel()
            {
                Bio = bio,
                Socials = socials
            };

            return View(footerViewModel);
        }
    }
}
