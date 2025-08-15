namespace PetShopTaskMVC.DataContext.Entities
{
    public class ProductTag:BaseEntity
    {
        public int TagId { get; set; }
        public Tag? Tag { get; set; }
        public int ProductId { get; set; }
        public Product? Product { get; set; }
    }


}
