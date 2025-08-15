using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using PetShopTaskMVC.DataContext;
using PetShopTaskMVC.Models;

namespace PetShopTaskMVC.Controllers
{
    public class BasketController : Controller
    {
        private const string Basket_KeyP = "basketP";
        private readonly AppDbContext _dbContext;

        public BasketController(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IActionResult Index()
        {
            return View();
        }

        public List<BasketCookieItemModel> AddBasketItemToBasket(int id)
        {
            var basketItems = GetBasketItems();

            var existedBasketItem = basketItems.Find(x => x.ProductId == id);
            if (existedBasketItem == null)
                basketItems.Add(new BasketCookieItemModel
                {
                    ProductId = id,
                });
            else
                existedBasketItem.Count++;

            return basketItems;
        }

        public List<BasketCookieItemModel> GetBasketItems()
        {
            var basketItemsInString = Request.Cookies[Basket_KeyP];

            var basketItems = new List<BasketCookieItemModel>();

            if (!string.IsNullOrEmpty(basketItemsInString))
            {
                basketItems = JsonConvert.DeserializeObject<List<BasketCookieItemModel>>(basketItemsInString);
            }

            return basketItems!;
        }

        public BasketViewModel GetBasketViewModelFromCookie(List<BasketCookieItemModel> basketCookieItemModels)
        {
            var basketViewModel = new BasketViewModel();
            var basketItemViewModels = new List<BasketItemViewModel>();

            foreach (var item in basketCookieItemModels)
            {
                var product = _dbContext.Products.Find(item.ProductId);

                if (product == null)
                    continue;

                basketItemViewModels.Add(new BasketItemViewModel
                {
                    Id = product.Id,
                    Price = product.Price,
                    Name = product.Name,
                    Count = item.Count,
                    CoverImageUrl = product.CoverImageUrl,
                });
            }

            basketViewModel.Items = basketItemViewModels;
            basketViewModel.Count = basketItemViewModels.Sum(x => x.Count);
            basketViewModel.Total = basketItemViewModels.Sum(x => x.Count * x.Price);

            return basketViewModel;
        }

        public IActionResult AddToBasket(int id)
        {
            var product = _dbContext.Products.Find(id);

            if (product == null)
                return BadRequest();    

            var basketItems = AddBasketItemToBasket(id);

            var basketItemsInJson = JsonConvert.SerializeObject(basketItems);

            Response.Cookies.Append(Basket_KeyP, basketItemsInJson,
                new CookieOptions { Expires = DateTimeOffset.Now.AddDays(1) });

            var basketViewModel = GetBasketViewModelFromCookie(basketItems);
                return Json(basketViewModel);
        }

        public IActionResult InitBasket()
        {
            var basketItemsFromCookie = GetBasketItems();
            var basketViewModel = GetBasketViewModelFromCookie(basketItemsFromCookie);

            return Json(basketViewModel);
        }
    }
}
