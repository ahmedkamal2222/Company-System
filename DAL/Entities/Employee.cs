using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Entities
{
    public class Employee : BaseEntity
    {
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
        [DisplayName("Department ID")]
        public int DepartmentId { get; set; }
        public Department? Department { get; set; }
    }
}
