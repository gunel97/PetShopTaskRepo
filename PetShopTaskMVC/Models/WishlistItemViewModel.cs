namespace PetShopTaskMVC.Models
{
    public class WishlistItemViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string CoverImageUrl { get; set; } = null!;
        public decimal Price { get; set; } 
    }
}
