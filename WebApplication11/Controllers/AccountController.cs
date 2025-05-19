using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages;
using WebApplication11.Models;
using WebApplication11.ViewModels;

namespace WebApplication11.Controllers
{
    public class AccountController : Controller
    {
        UserManager<AppUser> _userManage;
        SignInManager<AppUser> _signInManager;
        RoleManager<IdentityRole> _roleManager;
        public AccountController(UserManager<AppUser> userManage, SignInManager<AppUser> signInManager, RoleManager<IdentityRole> roleManager)
        {
            _userManage = userManage;
            _signInManager = signInManager;

            _roleManager = roleManager;
        }
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterVm registerVm)
        {
            if (!ModelState.IsValid)
            {
                return View(registerVm);
            }
            AppUser appUser = new AppUser()
            {
                Name = registerVm.Name,
                Email = registerVm.Email,
                Surname = registerVm.Surname,
                UserName = registerVm.UserName,
            };
            var result = await _userManage.CreateAsync(appUser, registerVm.Password);

            if (!result.Succeeded)
            {
                foreach (var item in result.Errors)
                {
                    ModelState.AddModelError("", item.Description);

                }
                return View(registerVm);
            }


            return RedirectToAction("Login");
            
        }
        public async Task<IActionResult> LogOut()
        {
            
           await  _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");


        }

        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginVm loginvm, string? returnUrl)
        {
            if (!ModelState.IsValid)
            { 
                return View();
            }
            AppUser user= await _userManage.FindByEmailAsync(loginvm.Email).ConfigureAwait(false);
            
            if (user != null)
            {
                var result = await _signInManager.PasswordSignInAsync(user , loginvm.Password!, loginvm.IsRememberMe, lockoutOnFailure: false);
                if (result.Succeeded)
                    if (returnUrl != null)
                        return RedirectPermanent(returnUrl!);
                return RedirectToAction("Index", "Home");

            }

            return View();
        }

        public async Task<IActionResult> CreateRole()
        {
            await _roleManager.CreateAsync(new IdentityRole("Admin"));
            await _roleManager.CreateAsync(new IdentityRole("Member"));



            return RedirectToAction("Index", "Home");
        }


    }
}
