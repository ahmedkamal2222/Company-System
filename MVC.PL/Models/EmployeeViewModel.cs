using DAL.Entities;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace MVC.PL.Models
{
    public class EmployeeViewModel
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(100)]
        [DisplayName("User Name")]

        public string? Name { get; set; }
        public string? Address { get; set; }
        public decimal Salary { get; set; }
        public bool IsActive { get; set; }
        [EmailAddress]
        public string? Email { get; set; }
        public DateTime HireDate { get; set; }
        public string? Image { get; set; }
        public IFormFile ImageSource { get; set; }
        [DisplayName("Department ID")]

        public int DepartmentId { get; set; }

    }
}
