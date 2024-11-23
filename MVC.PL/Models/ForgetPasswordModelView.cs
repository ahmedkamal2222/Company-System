using System.ComponentModel.DataAnnotations;

namespace MVC.PL.Models
{
    public class ForgetPasswordModelView
    {
        [Required]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string Email { get; set; }
    }
}
