using PetShopTaskMVC.DataContext.Entities;

namespace PetShopTaskMVC.Models
{
    public class FooterViewModel
    {
        public Bio? Bio { get; set; }
        public List<Social> Socials { get; set; } = [];
    }
}
