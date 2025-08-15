using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;

namespace PetShopTaskMVC.DataContext.Entities
{
    public class Bio:BaseEntity
    {
        public string LogoUrl { get; set; } = null!;
        public string PhoneNumber {  get; set; } = null!;
        public string EmailAddress { get; set; } = null!;
    }
}
