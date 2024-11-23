using AutoMapper;
using DAL.Entities;
using MVC.PL.Controllers;
using MVC.PL.Models;

namespace MVC.PL.Mapper
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<EmployeeViewModel, Employee>().ReverseMap();
            CreateMap<DepartmentViewModel, Department>().ReverseMap();
            //CreateMap<Employee, EmployeeViewModel>();
        }
    }
}
