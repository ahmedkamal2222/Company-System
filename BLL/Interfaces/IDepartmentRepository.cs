using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interfaces
{
    public interface IDepartmentRepository : IGenericRepository<Department>
    {
        // - Put Here AnyThing Is Not Common !!

        IEnumerable<Department> SearchByName(string name);


        //Department GetDepartmentById(int? id);
        //IEnumerable<Department> GetAllDepartments();
        //int Add(Department department);
        //int Update(Department department);
        //int Delete(Department department);
    }
}
