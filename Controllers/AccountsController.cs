using book.Models;
using book.ViewModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace book.Controllers
{
    public class AccountsController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        public AccountsController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }
        public IActionResult login()
        {
            Console.WriteLine("new line");
            return View();
        }
        [HttpPost]
        public IActionResult login(loginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = _userManager.FindByEmailAsync(model.email).Result;
                if (user is not null)
                {
                    if (_userManager.CheckPasswordAsync(user, model.password).Result)
                    {
                        var res = _signInManager.PasswordSignInAsync(user, model.password, false, false).Result;
                        if (res.Succeeded)
                        {
                            return RedirectToAction("AllBooks", "Home");
                        }
                    }
                }
            }
            ModelState.AddModelError(string.Empty, "Incorrect email or password");
            return View();
        }
        public IActionResult register()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> register(registerViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser
                {
                    UserName = model.username,
                    Email = model.email,
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                };
                var result = _userManager.CreateAsync(user, model.password).Result;
                await _userManager.AddToRoleAsync(user, ClassRoles.roleUser);
                if (result.Succeeded)
                {

                    return RedirectToAction("login", "Accounts");
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }
            return View();
        }
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("AllBooks", "Home");
        }
    }
}
