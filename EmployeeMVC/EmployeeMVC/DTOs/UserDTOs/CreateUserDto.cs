using System.ComponentModel.DataAnnotations;

namespace EmployeeMVC.DTOs.UserDTOs
{
    public class CreateUserDto
    {
        [Required]
        [MinLength(3)]
        [MaxLength(25)]
        [Display(Prompt = "FirstName")]
        public string FirstName { get; set; }
        [Required]
        [MinLength(3)]
        [MaxLength(25)]
        [Display(Prompt = "LastName")]
        public string LastName { get; set; }
        [Required]
        [DataType(DataType.EmailAddress)]
        [Display(Prompt = "Email")]

        public string Email { get; set; }
        [Required]
        [Length(3, 30)]
        [Display(Prompt = "UserName")]
        public string UserName { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Prompt = "Password")]
        public string Password { get; set; }
        [Required]
        [DataType(DataType.Password)]
        [Display(Prompt = "Password Again")]
        public string ConfirmPassword { get; set; }
    }
}
