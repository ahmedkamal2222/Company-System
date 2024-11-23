using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Entities
{
    public class Department : BaseEntity
    {

        [Required(ErrorMessage = " Code Is Reqiure")]
        public string Code { get; set; }
        [Required(ErrorMessage = "  Is Reqiure")]
        [MaxLength(100)]
        public string Name { get; set; }
        public DateTime DateOfCreation { get; set; }
    }
}
