namespace PetShopTaskMVC.Models
{
    public class BasketViewModel
    {
        public int Count { get; set; }
        public decimal Total { get; set; }
        public List<BasketItemViewModel> Items { get; set; } = [];
    }
}
