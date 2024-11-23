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
    public class EmployeeRepository : GenericRepository<Employee>, IEmployeeRepository
    {
        private readonly MvcAppDbContext _Context;

        public EmployeeRepository(MvcAppDbContext context) : base(context)
        {
            _Context = context;
        }


        public IEnumerable<Employee> GetEmployeesByName(string name)
        {
            _Context.Employees.Where(emp => emp.Name == name).ToList();
            return _Context.Employees;
        }

        public IEnumerable<Employee> SearchByDepartmentId(int? id)
            => _Context.Employees.Where(emp =>emp.DepartmentId == id).ToList();

        public IEnumerable<Employee> SearchByName(string name)
            => _Context.Employees.Where(emp => emp.Name.Trim().ToLower().Contains(name.Trim().ToLower()));
    }
}
