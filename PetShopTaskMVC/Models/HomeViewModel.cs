using PetShopTaskMVC.DataContext.Entities;

namespace PetShopTaskMVC.Models
{
    public class HomeViewModel
    {
        public List<Product> Products { get; set; } = [];
        public List<Category> Categories { get; set; } = [];
        public List<Product> PetClothes { get; set; } = [];
        public List<Product> PetFoods { get; set; } = [];
    }
}
