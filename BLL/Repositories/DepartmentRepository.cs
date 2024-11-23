using BLL.Interfaces;
using DAL.Context;
using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Repositories
{
    public class DepartmentRepository : GenericRepository<Department>, IDepartmentRepository
    {
        private readonly MvcAppDbContext _Context;

        public DepartmentRepository(MvcAppDbContext context) : base(context)
        {
            _Context = context;
        }
        public IEnumerable<Department> SearchByName(string name)
                => _Context.Departments.Where(dept => dept.Name.Trim().ToLower().Contains(name.Trim().ToLower()));
    }
}
