namespace PetShopTaskMVC.DataContext.Entities
{
    public class ProductImage : BaseEntity
    {
        public required string Name { get; set; }
        public int ProductId { get; set; }
        public Product? Product { get; set; }
    }


}
