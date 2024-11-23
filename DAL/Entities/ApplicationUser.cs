using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Entities
{
    public class ApplicationUser : IdentityUser
    {
        public bool IsAgree { get; set; }
    }
}
