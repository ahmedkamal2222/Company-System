using System.ComponentModel.DataAnnotations;

namespace MVC.PL.Models
{
    public class RegisterViewModel
    {
        [Required]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string Email { get; set; }
        [Required]
        [StringLength(10, MinimumLength = 6)]
        public string Password { get; set; }
        [Required]
        [Compare(nameof(Password), ErrorMessage ="Password MisMatch")]
        public string ConfirmPassword { get; set; }
        [Required]
        public bool IsAgree { get; set; }
    }
}
