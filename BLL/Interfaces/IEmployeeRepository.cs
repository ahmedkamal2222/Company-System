using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interfaces
{
    public interface IEmployeeRepository : IGenericRepository<Employee>
    {
        // - Put Here AnyThing Is Not Common !!

        IEnumerable<Employee> GetEmployeesByName(string name);
        IEnumerable<Employee> SearchByDepartmentId(int? id);
        IEnumerable<Employee> SearchByName(string name);

        //Employee GetEmployeeById(int? id);
        //IEnumerable<Employee> GetAllEmployees();
        //int Add(Employee employee);
        //int Update(Employee employee);
        //int Delete(Employee employee);
    }
}
