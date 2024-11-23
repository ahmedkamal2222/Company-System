using System.ComponentModel.DataAnnotations;

namespace MVC.PL.Models
{
    public class DepartmentViewModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = " Code Is Reqiure!")]
        public string? Code { get; set; }
        [Required(ErrorMessage = " Name Is Reqiure!")]
        [MaxLength(100)]
        public string? Name { get; set; }
        public DateTime DateOfCreation { get; set; }
    }
}
