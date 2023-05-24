using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using SiimpleWebApp.Models;
using SiimpleWebApp.ViewModels.AccountVM;

namespace SiimpleWebApp.Controllers
{
    public class AccountController:Controller
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
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterVM registerVM)
        {
            if(!ModelState.IsValid) return View(registerVM);

            AppUser appUser = new AppUser()
            {
                Name=registerVM.Name,
                Surname=registerVM.Surname,
                Email=registerVM.Email,
                UserName=registerVM.UserName
            };

            IdentityResult identityResult= await _userManager.CreateAsync(appUser,registerVM.Password);
            if(!identityResult.Succeeded)
            {
                foreach(IdentityError? error in identityResult.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                    return View(registerVM);
                }
            }

            #region Add Role
            //IdentityResult result = await _userManager.AddToRoleAsync(appUser, "Admin");
            //if (!result.Succeeded)
            //{
            //    foreach (IdentityError? error in result.Errors)
            //    {
            //        ModelState.AddModelError("", error.Description);
            //        return View(registerVM);
            //    }
            //}
            #endregion

            return RedirectToAction(nameof(Login));
        }
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginVM login)
        {
            if (!ModelState.IsValid) return View(login);

            AppUser user=   await _userManager.FindByNameAsync(login.UserName);
            if (user == null)
            {
                ModelState.AddModelError("", "Invalid username or password.");
                return View(login);
            }

            Microsoft.AspNetCore.Identity.SignInResult result=  await _signInManager.PasswordSignInAsync(user, login.Password, true, false);
            if (!result.Succeeded)
            {
                ModelState.AddModelError("", "Invalid username or password.");
                return View(login);
            }

            return RedirectToAction("Index", "Home");
        }
        public async Task<IActionResult> LogOut()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

        #region Create Role
        //public async Task< IActionResult> CreateRole()
        //{
        //    IdentityRole role = new IdentityRole()
        //    {
        //        Name = "Admin"
        //    };
         
        //    IdentityResult result =await  _roleManager.CreateAsync(role);
        //    if(!result.Succeeded) return NotFound();

        //    return Json("OK");
        //}
        #endregion
    }
}
