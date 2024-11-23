using DAL.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MVC.PL.Helper;
using MVC.PL.Models;

namespace MVC.PL.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public AccountController(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        #region SignUp
        public IActionResult SignUp()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> SignUp(RegisterViewModel registerViewModel)
        {
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser()
                {
                    Email = registerViewModel.Email,
                    UserName = registerViewModel.Email.Split('@')[0],
                    IsAgree = registerViewModel.IsAgree
                };

                var result = await _userManager.CreateAsync(user, registerViewModel.Password);

                if (result.Succeeded)
                    return RedirectToAction("LogIn");

                foreach (var error in result.Errors)
                    ModelState.AddModelError(string.Empty, error.Description);
            }

            return View(registerViewModel);
        }
        #endregion

        #region LogIn
        public IActionResult LogIn()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> LogIn(LogInViewModel logInViewModel)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(logInViewModel.Email);

                if (user is null)
                    ModelState.AddModelError("", "Email Dosn't Exist!");

                var isCorrectPassword = await _userManager.CheckPasswordAsync(user, logInViewModel.Password);

                if (isCorrectPassword)
                {
                    var result = await _signInManager.PasswordSignInAsync(user, logInViewModel.Password, logInViewModel.RemmemberMe, false);

                    if (result.Succeeded)
                        return RedirectToAction("Index", "Home");
                }
            }

            return View(logInViewModel);
        }
        #endregion

        #region SignOut
        public new async Task<IActionResult> SignOut()
        {
            await _signInManager.SignOutAsync();

            return RedirectToAction("LogIn");
        }
        #endregion

        #region ForgotPassword

        public IActionResult ForgetPassword()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> ForgetPassword(ForgetPasswordModelView forgetPasswordModelView)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(forgetPasswordModelView.Email);

                if (user != null)
                {
                    var token = await _userManager.GeneratePasswordResetTokenAsync(user);

                    var resetPasswprdLink = Url.Action("ResetPassword", "Account", new { Email = forgetPasswordModelView.Email, Token = token }, Request.Scheme);

                    var email = new Email
                    {
                        Title = "Reset Password",
                        Body = resetPasswprdLink,
                        To = forgetPasswordModelView.Email
                    };

                    SendEmailSetting.SendEmail(email);

                    return RedirectToAction("CompletForgetPassword");

                }

                ModelState.AddModelError("", "Invalid Email !");
            }

            return View(forgetPasswordModelView);

        }

        public IActionResult CompletForgetPassword()
        {
            return View();
        }

        #endregion

        #region Reset Password
        public IActionResult ResetPassword(string email, string token)
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ResetPassword(ResetPasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(model.Email);

                if (user != null)
                {
                    var result = await _userManager.ResetPasswordAsync(user, model.Token, model.Password);

                    if (result.Succeeded)
                        return RedirectToAction(nameof(LogIn));

                    foreach (var error in result.Errors)
                        ModelState.AddModelError(string.Empty, error.Description);
                }
            }
            return View(model);
        } 
        #endregion

        public IActionResult AccessDenied() 
        {
            return View();    
        }

    }
}
