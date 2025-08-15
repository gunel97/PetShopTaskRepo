using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Newtonsoft.Json;
using PetShopTaskMVC.DataContext;
using PetShopTaskMVC.Models;

namespace PetShopTaskMVC.Controllers
{
    public class WishlistController:Controller
    {
        private const string Wishlist_Key = "Wishlist";
        private readonly AppDbContext _dbContext;

        public WishlistController(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IActionResult Index()
        {
            var wishlistItems = GetWishlistItems();
            var wishlistViewModel = GetWishlistViewModelFromCookie(wishlistItems);
            return View(wishlistViewModel);
        }

        public List<WishlistCookieItemModel> AddWishlistItemToList(int id)
        {
            var wishlistItems = GetWishlistItems();

            var existedWishlistItem = wishlistItems.Find(x => x.ProductId == id);

            if (existedWishlistItem == null)
            {
                wishlistItems.Add(new WishlistCookieItemModel
                {
                    ProductId = id
                });
            }

            return wishlistItems;
        }

        public List<WishlistCookieItemModel> GetWishlistItems()
        {
            var wishlistItemsInString = Request.Cookies[Wishlist_Key];

            var wishlistItems = new List<WishlistCookieItemModel>();

            if(!string.IsNullOrEmpty(wishlistItemsInString))
            {
                wishlistItems = JsonConvert.DeserializeObject<List<WishlistCookieItemModel>>(wishlistItemsInString);
            }

            return wishlistItems!;
        }

        public WishlistViewModel GetWishlistViewModelFromCookie(List<WishlistCookieItemModel> wishlistCookieItemModels)
        {
            var wishlistViewModel = new WishlistViewModel();
            var wishlistItemViewModels = new List<WishlistItemViewModel>();
            
            foreach(var item in wishlistCookieItemModels)
            {
                var product = _dbContext.Products.Find(item.ProductId);
                if (product == null)
                    continue;

                wishlistItemViewModels.Add(new WishlistItemViewModel
                {
                    Id = product.Id,
                    Name = product.Name,
                    CoverImageUrl = product.CoverImageUrl,
                    Price = product.Price,
                });
            }

            wishlistViewModel.WishlistItems = wishlistItemViewModels;

            return wishlistViewModel;
        }

        public IActionResult AddToWishlist(int id)
        {
            var product = _dbContext.Products.Find(id);

            if (product == null)
                return BadRequest();

            var wishlistItems = AddWishlistItemToList(id);

            var wishlistItemsInJson = JsonConvert.SerializeObject(wishlistItems);
            
            Response.Cookies.Append(Wishlist_Key, wishlistItemsInJson, 
                new CookieOptions { Expires=DateTimeOffset.UtcNow.AddDays(1) });

            var wishlistViewModel = GetWishlistViewModelFromCookie(wishlistItems);

            return Json(wishlistViewModel);
        }

        public List<WishlistCookieItemModel> RemoveBasketItemFromWishlist(int id)
        {
            var wishlistItemsFromCookie = GetWishlistItems();
            var itemIndex = wishlistItemsFromCookie.FindIndex(x => x.ProductId == id);
            if (itemIndex != -1)
                wishlistItemsFromCookie.RemoveAt(itemIndex);

            return wishlistItemsFromCookie;
        }

        public IActionResult RemoveFromWishlist(int id)
        {
            var wishlistItemsFromCookie = RemoveBasketItemFromWishlist(id);
            var wishlistItemsInJson = JsonConvert.SerializeObject(wishlistItemsFromCookie);

            Response.Cookies.Append(Wishlist_Key, wishlistItemsInJson,
                new CookieOptions
                {
                    Expires = DateTimeOffset.Now.AddDays(1)
                });

            var wishlistViewModel = GetWishlistViewModelFromCookie(wishlistItemsFromCookie);
            return Json(wishlistViewModel);
        }
    }
}
