using System.ComponentModel.DataAnnotations;

namespace MVC.PL.Models
{
    public class LogInViewModel
    {
        [Required]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string Email { get; set; }
        [Required]
        [StringLength(10, MinimumLength = 6)]
        public string Password { get; set; }

        public bool RemmemberMe { get; set; }
    }
}
