using Microsoft.AspNetCore.Identity;

namespace PetShopTaskMVC.DataContext.Entities
{
    public class AppUser:IdentityUser
    {
        public string? FullName { get; set; }
    }
}
