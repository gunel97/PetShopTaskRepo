using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using System.ComponentModel.DataAnnotations;

namespace PetShopTaskMVC.Models
{
    public class RegisterViewModel
    {
        public string? FullName { get; set; }
        public required string Username {  get; set; }
        [DataType(DataType.EmailAddress)]
        public required string Email { get; set; }
        [DataType(DataType.Password)]
        public required string Password { get; set; }
        [DataType(DataType.Password)]
        [Compare("Password")]
        public required string ConfirmPassword {  get; set; }

    }
}
