using AutoMapper;
using BLL.Interfaces;
using DAL.Context;
using DAL.Entities;
using Microsoft.AspNetCore.Mvc;
using MVC.PL.Models;
using System.Security.Permissions;

namespace MVC.PL.Controllers
{
    public class DepartmentController : Controller
    {
        //private readonly IDepartmentRepository _departmentRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public DepartmentController(/*IDepartmentRepository departmentRepository,*/ IUnitOfWork unitOfWork, IMapper mapper)
        {

            //_departmentRepository = departmentRepository;
           _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        //public IActionResult Index()
        //{

        //    var departments = _unitOfWork.DepartmentRepository.GetAll();
        //    var departmentViewModel = _mapper.Map<IEnumerable<DepartmentViewModel>>(departments);
        //    return View(departmentViewModel);
        //}

        public IActionResult Index(string searchvalue = "")
        {
            IEnumerable<Department> departments;
            IEnumerable<DepartmentViewModel> departmentViewModels;
            if (string.IsNullOrWhiteSpace(searchvalue))
            {
                departments = _unitOfWork.DepartmentRepository.GetAll();
                departmentViewModels = _mapper.Map<IEnumerable<DepartmentViewModel>>(departments);
            }
            else
            {
                departments = _unitOfWork.DepartmentRepository.SearchByName(searchvalue);
                departmentViewModels = _mapper.Map<IEnumerable<DepartmentViewModel>>(departments);

            }
            return View(departmentViewModels);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(DepartmentViewModel departmentViewModel)
        {
            if (ModelState.IsValid)
            {
                var department = _mapper.Map<Department>(departmentViewModel);
                _unitOfWork.DepartmentRepository.Add(department);

                return RedirectToAction("Index");
            }
            return View(departmentViewModel);
        }

        public IActionResult Details(int? id)
        {
            if (id is null)
                return RedirectToAction("Error", "Home");

            var department = _unitOfWork.DepartmentRepository.GetById(id);        

            if (department == null)
                return RedirectToAction("Error", "Home");

            var departmentViewModel = _mapper.Map<DepartmentViewModel>(department);
            return View(departmentViewModel);

        }

        public IActionResult Update(int? id)
        {
            if (id is null)
                return NotFound();

            var department = _unitOfWork.DepartmentRepository.GetById(id);

            if (department == null)
                return NotFound();

            var departmentViewModel = _mapper.Map<DepartmentViewModel>(department);
            return View(departmentViewModel);

        }

        [HttpPost]
        public IActionResult Update(int? id, DepartmentViewModel departmentViewModel)
        {
            if (id == null)
                return NotFound();
            try
            {
                if (ModelState.IsValid)
                {
                    var department = _mapper.Map<Department>(departmentViewModel);
                    _unitOfWork.DepartmentRepository.Update(department);
                    return RedirectToAction("Index");
                }

            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }

            return View(departmentViewModel);

        }

        public IActionResult Delete(int? id)
        {
            if (id is null)
                return NotFound();

            var department = _unitOfWork.DepartmentRepository.GetById(id);


            if (department == null)
                return NotFound();

            _unitOfWork.DepartmentRepository.Delete(department);
            return RedirectToAction("Index");

        }
    }
}
