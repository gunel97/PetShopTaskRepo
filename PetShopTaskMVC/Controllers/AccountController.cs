using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.DiaSymReader;
using PetShopTaskMVC.DataContext.Entities;
using PetShopTaskMVC.Models;
using System.Resources;
using System.Threading.Tasks;

namespace PetShopTaskMVC.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public AccountController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var user = new AppUser
            {
                UserName = model.Username,
                Email = model.Email,
                FullName = model.FullName,
            };

            var result = await _userManager.CreateAsync(user, model.Password);

            if (!result.Succeeded)
            {
                foreach (var item in result.Errors)
                {
                    ModelState.AddModelError("", item.Description);
                }

                return View(model);
            }

            var adminRoleResult = await _roleManager.CreateAsync(new IdentityRole
            {
                Name = "Admin"
            });

            if (adminRoleResult.Succeeded) {
                await _userManager.AddToRoleAsync(user, "Admin");
            }

            return RedirectToAction("Index", "Home");
        }

        public IActionResult Login()
        {
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var user = await _userManager.FindByNameAsync(model.Username);

            if (user == null)
            {
                ModelState.AddModelError("", "Username or password is incorrect.");

                return View(model);
            }

            var result = await _signInManager.PasswordSignInAsync(user, model.Password, model.RememberMe, true);

            if (result.IsLockedOut)
            {
                ModelState.AddModelError("", $"You are banned {user.LockoutEnd.Value.AddHours(4).ToString()}");

                return View(model);
            }

            if (!result.Succeeded)
            {
                ModelState.AddModelError("", "Username or password is incorrect.");

                return View(model);
            }

            if (!string.IsNullOrEmpty(model.ReturnUrl))
                return Redirect(model.ReturnUrl);

            return RedirectToAction("Index", "Home");
        }

        public IActionResult AccessDenied()
        {
            return View();
        }

        [Authorize]
        public IActionResult ChangePassword()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ChangePassword(ChangePasswordViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var username = User.Identity!.Name ?? "";

            var user = await _userManager.FindByNameAsync(username);

            if (user == null)
            {
                return BadRequest();
            }

            var result = await _userManager.ChangePasswordAsync(user, model.CurrentPassword, model.NewPassword);

            if (!result.Succeeded)
            {
                foreach (var item in result.Errors)
                {
                    ModelState.AddModelError("", item.Description);
                }

                return View(model);
            }

            return RedirectToAction(nameof(Login));
        }

        public IActionResult ForgotPassword()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ForgotPassword(string email)
        {
            if (string.IsNullOrEmpty(email))
            {
                ModelState.AddModelError("", "Email is required");

                return View();
            }

            var user = await _userManager.FindByEmailAsync(email);

            if (user == null)
            {
                ModelState.AddModelError("", "Email not found");

                return View();
            }

            var resetPasswordToken = await _userManager.GeneratePasswordResetTokenAsync(user);
            var resetLink = Url.Action("ResetPassword", "Account", new { email, resetPasswordToken },
                Request.Scheme, Request.Host.ToString());

            return View("EmailSimulation", resetLink);
        
        }

        public IActionResult ResetPassword()
        {
            return View();
        }

        [HttpPost]
        //public async Task<IActionResult> ResetPassword(ResetPasswordViewModel model)
        //{
        //    if(!ModelState.IsValid)
        //    {
        //        return View(model);
        //    }

        //    var user = await _userManager.FindByEmailAsync(model.Email);

        //    if (user == null)
        //    {
        //        return View(model);
        //    }

        //    var result = await _userManager.ResetPasswordAsync(user, model.ResetPasswordToken,
        //        model.Password);

        //    if (!result.Succeeded)
        //    {
        //        foreach(var item in result.Errors)
        //        {
        //            ModelState.AddModelError("", item.Description);

        //            return View(model);
        //        }
        //    }

        //    RedirectToAction(nameof(Login));

        //}

        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();

            return RedirectToAction("Index", "Home");
        }
    }
}