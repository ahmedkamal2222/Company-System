using DAL.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace MVC.PL.Controllers
{
    [Authorize(Roles = "Admin")]
    public class UsersController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ILogger _logger;

        public UsersController(UserManager<ApplicationUser> userManager, ILogger<ApplicationUser> logger)
        {
            _userManager = userManager;
            _logger = logger;
        }
        public async Task<IActionResult> Index(string searchValue = "")
        {
            List<ApplicationUser> users;

            if (string.IsNullOrEmpty(searchValue))
            {
                users = await _userManager.Users.ToListAsync();
            }
            else
            {
                users = await _userManager.Users.Where(user => user.Email.Trim().ToLower().Contains(searchValue.Trim().ToLower())).ToListAsync();
            }
            return View(users);
        }

        public async Task<IActionResult> Details(string id, string viewName = "Details")
        {
            if (id is null)
                return NotFound();

            var user = await _userManager.FindByIdAsync(id);

            if (user is null)
                return NotFound();

            return View(viewName, user);

        }

        public async Task<IActionResult> Update(string id)
        {
            return await Details(id, "Update");
        }
        [HttpPost]
        public async Task<IActionResult> Update(string id, ApplicationUser UserApp)
        {
            if (id != UserApp.Id) 
                return NotFound(id);

            if (ModelState.IsValid)
            {
                try
                {
                    var user = await _userManager.FindByIdAsync(id);

                    user.UserName = UserApp.UserName;
                    user.NormalizedUserName = UserApp.UserName.ToUpper();
                    user.PhoneNumber = UserApp.PhoneNumber;

                    var result = await _userManager.UpdateAsync(user);

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

                return View(UserApp);
        }

        public async Task<IActionResult> Delete(string id, ApplicationUser UserApp)
        {
            if (id != UserApp.Id)
                return NotFound(id);

            if (ModelState.IsValid)
            {
                try
                {
                    var user = await _userManager.FindByIdAsync(id);

                    var result = await _userManager.DeleteAsync(user);

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

    }
}
