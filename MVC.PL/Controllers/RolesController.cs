using DAL.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MVC.PL.Models;
using System.Data;

namespace MVC.PL.Controllers
{
    [Authorize(Roles = "Admin")]
    public class RolesController : Controller
    {
        private readonly RoleManager<ApplicationRole> _roleManager;
        private readonly ILogger<ApplicationRole> _logger;
        private readonly UserManager<ApplicationUser> _userManager;

        public RolesController(RoleManager<ApplicationRole> roleManager, 
            ILogger<ApplicationRole> logger,
            UserManager<ApplicationUser> userManager)
        {
            _roleManager = roleManager;
            _logger = logger;
            _userManager = userManager;
        }
        public async Task<IActionResult> Index(string searchValue = "")
        {
            IEnumerable<ApplicationRole> roles;
            if (string.IsNullOrEmpty(searchValue))
            {
                  roles = await _roleManager.Roles.ToListAsync();
            }
            else
            {
                 roles = await _roleManager.Roles.Where(role => role.Name.Trim().ToLower().Contains(searchValue.Trim().ToLower())).ToListAsync();
            }

            return View(roles);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(ApplicationRole roleApp)
        {
            if (ModelState.IsValid)
            {
                var result = await _roleManager.CreateAsync(roleApp);

                if (result.Succeeded)
                    return RedirectToAction("Index");

                foreach (var error in result.Errors)
                    ModelState.AddModelError(string.Empty, error.Description);
            }
            return View(roleApp);
        }

        public async Task<IActionResult> Details(string id, string viewName = "Details")
        {
            if (id is null)
                return NotFound();

            var user = await _roleManager.FindByIdAsync(id);

            if (user is null)
                return NotFound();

            return View(viewName, user);

        }

        public async Task<IActionResult> Update(string id)
        {
            return await Details(id, "Update");
        }
        [HttpPost]
        public async Task<IActionResult> Update(string id, ApplicationRole roleApp)
        {
            if (id != roleApp.Id)
                return NotFound(id);

            if (ModelState.IsValid)
            {
                try
                {
                    var role = await _roleManager.FindByIdAsync(id);

                    role.Name = roleApp.Name;
                    role.NormalizedName = roleApp.Name.ToUpper();

                    var result = await _roleManager.UpdateAsync(role);

                    if (result.Succeeded)
                        return RedirectToAction(nameof(Index));

                    foreach (var error in result.Errors)
                        ModelState.AddModelError(string.Empty, error.Description);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex.Message);
                }
            }

            return View(roleApp);
        }

        public async Task<IActionResult> Delete(string id, ApplicationRole appRole)
        {
            if (id != appRole.Id)
                return NotFound(id);

            if (ModelState.IsValid)
            {
                try
                {
                    var role = await _roleManager.FindByIdAsync(id);

                    var result = await _roleManager.DeleteAsync(role);

                    if (result.Succeeded)
                        return RedirectToAction(nameof(Index));

                    foreach (var error in result.Errors)
                        ModelState.AddModelError(string.Empty, error.Description);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex.Message);
                }
            }

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> AddOrRemoveUsers(string roleId)
        {
            ViewBag.RoleId = roleId;
            var role = await _roleManager.FindByIdAsync(roleId);

            if (role is null)
                return NotFound();

            var usersInRole = new List<UsersInRoleViewModel>();

            foreach (var user in await _userManager.Users.ToListAsync())
            {
                var userInRole = new UsersInRoleViewModel
                {
                    UserName = user.UserName,
                    UserId = user.Id
                };

                if (await _userManager.IsInRoleAsync(user, role.Name))
                    userInRole.IsSelected = true;

                else
                    userInRole.IsSelected = false;

                usersInRole.Add(userInRole);
            }

            return View(usersInRole);
        }
        [HttpPost]
        public async Task<IActionResult> AddOrRemoveUsers(List<UsersInRoleViewModel> users,string roleId)
        {
            var role = await _roleManager.FindByIdAsync(roleId);

            if (role is null)
                return NotFound(roleId);


            if (ModelState.IsValid)
            {
                foreach (var user in users)
                {
                    var appUser = await _userManager.FindByIdAsync(user.UserId);

                    if (appUser != null)
                    {
                        if (user.IsSelected && !(await _userManager.IsInRoleAsync(appUser, role.Name)))
                            await _userManager.AddToRoleAsync(appUser, role.Name);

                        else if (!user.IsSelected && (await _userManager.IsInRoleAsync(appUser,role.Name)))
                            await _userManager.RemoveFromRoleAsync(appUser, role.Name);
                    }

                }
                    return RedirectToAction (nameof(Update), new {id = roleId});

            }
                return View(users);
        }
    }
}

