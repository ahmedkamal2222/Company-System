using AutoMapper;
using BLL.Interfaces;
using DAL.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis;
using MVC.PL.Helper;
using MVC.PL.Models;

namespace MVC.PL.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public EmployeeController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        //public IActionResult Index(string searchvalue = "")
        //{
        //    IEnumerable<Employee> employees;
        //    IEnumerable<EmployeeViewModel> employeesViewModel;
        //    if (string.IsNullOrEmpty(searchvalue))
        //    {
        //        employees = _unitOfWork.EmployeeRepository.GetAll();
        //    }
        //    else
        //    {
        //        employees = _unitOfWork.EmployeeRepository.SearchByName(searchvalue);

        //    }
        //    employeesViewModel = _mapper.Map<IEnumerable<EmployeeViewModel>>(employees);
        //    return View(employeesViewModel);

        //}

        public IActionResult Index(int? departmnetId = null , string searchvalue = "")
        {
            IEnumerable<Employee> employees;
            IEnumerable<EmployeeViewModel> employeesViewModel;
            if ((departmnetId is null) && (string.IsNullOrEmpty(searchvalue)))
            {
                 employees = _unitOfWork.EmployeeRepository.GetAll();
            }
            else if ((!string.IsNullOrEmpty(searchvalue)) && (departmnetId is null))
            {
                employees = _unitOfWork.EmployeeRepository.SearchByName(searchvalue);
            }
            else
            {
                employees = _unitOfWork.EmployeeRepository.SearchByDepartmentId(departmnetId);
            }
            employeesViewModel = _mapper.Map<IEnumerable<EmployeeViewModel>>(employees);

            return View(employeesViewModel);
        }



        public IActionResult Create()
        {
            ViewBag.Departments = _unitOfWork.DepartmentRepository.GetAll();
            return View();
        }

        [HttpPost]
        public IActionResult Create(EmployeeViewModel employeesViewModel)
        {
            //ModelState["Department"].ValidationState = Microsoft.AspNetCore.Mvc.ModelBinding.ModelValidationState.Valid;
            if (ModelState.IsValid)
            {
                
                try
                {
                    var employee = _mapper.Map<Employee>(employeesViewModel);
                    employee.Image = DocumentSettings.UploadFile(employeesViewModel.ImageSource, "Images");
                    

                    _unitOfWork.EmployeeRepository.Add(employee);
                    return RedirectToAction(nameof(Index));

                }   
                catch (Exception ex)
                { 

                    throw new Exception(ex.Message);
                }
            }
            ViewBag.Departments = _unitOfWork.DepartmentRepository.GetAll();
            return View(employeesViewModel);
        }

        public IActionResult Details (int? id)
        {
            ViewBag.Departments = _unitOfWork.DepartmentRepository.GetAll();
            if (id is null)
                return NotFound();

            var employee = _unitOfWork.EmployeeRepository.GetById(id);
            var employeevm = _mapper.Map<EmployeeViewModel>(employee);
            if (employeevm == null)
                return NotFound();

            return View(employeevm);

        }

        public IActionResult Update(int? id)
        {
            ViewBag.Departments = _unitOfWork.DepartmentRepository.GetAll();
            if (id is null)
                return NotFound();

            var employees = _unitOfWork.EmployeeRepository.GetById(id);
            var employeevm = _mapper.Map<EmployeeViewModel>(employees);
            
            if (employeevm == null)
                return NotFound();

            return View(employeevm);

        }

        [HttpPost]
        public IActionResult Update(int? id, EmployeeViewModel employeesViewModel)
        {
            ViewBag.Departments = _unitOfWork.DepartmentRepository.GetAll();
            //ModelState["Department"].ValidationState = Microsoft.AspNetCore.Mvc.ModelBinding.ModelValidationState.Valid;
            if (id == null)
                return NotFound();
            try
            {
                if (ModelState.IsValid)
                {
                    var employee = _mapper.Map<Employee>(employeesViewModel);
                    employee.Image = DocumentSettings.UploadFile(employeesViewModel.ImageSource, "Images");
                    
                    _unitOfWork.EmployeeRepository.Update(employee);
                    return RedirectToAction("Index");
                }

            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }

            return View(employeesViewModel);

        }

        public IActionResult Delete (int? id)
        {
            if (id == null) 
                return NotFound();
            var employee = _unitOfWork.EmployeeRepository.GetById(id);

            if (employee == null)
                return NotFound();

            _unitOfWork.EmployeeRepository.Delete(employee);
            return RedirectToAction("Index");

        }
    }
}
