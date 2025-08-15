using PetShopTaskMVC.DataContext.Entities;

namespace PetShopTaskMVC.Models
{
    public class ShopViewModel
    {
        public List<Product> Products { get; set; } = [];
        public List<Category> Categories { get; set; } = [];
        public List<Tag> Tags { get; set; } = [];
    }
}
