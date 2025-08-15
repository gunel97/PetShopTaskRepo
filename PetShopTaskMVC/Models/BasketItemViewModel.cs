namespace PetShopTaskMVC.Models
{
    public class BasketItemViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public int Count { get; set; }
        public string CoverImageUrl { get; set; } = null!;
        public decimal Price {  get; set; }
    }
}
