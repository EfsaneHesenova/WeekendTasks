using System.ComponentModel.DataAnnotations;

namespace EmployeeMVC.DTOs.UserDTOs
{
    public class LoginUserDto
    {
        [Required]
        [Display(Prompt = "EmailOrUsername")]
        public string EmailOrUsername { get; set; }

        [DataType(DataType.Password), Required]
        [Display(Prompt = "Password")]
        public string Password { get; set; }
        [Required]
        public bool isPersistant { get; set; }
    }
}
