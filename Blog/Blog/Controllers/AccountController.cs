using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using BlogApp.Models;
using BlogApp.Models.ViewModels;

namespace BlogApp.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        private UserManager<AppUser> userManager;
        private SignInManager<AppUser> signManager;
        public AccountController(UserManager<AppUser> _userManager, SignInManager<AppUser> _signManager)
        {
            userManager = _userManager;
            signManager = _signManager;
        }

        public ViewResult Index()
        {
            return View();
        }

        [AllowAnonymous]
        [HttpGet]
        public ViewResult Login(string returnUrl)
        {
            return View(new LoginViewModel() { ReturnUrl = returnUrl });
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel loginViewModel)
        {
            if (ModelState.IsValid)
            {
                AppUser user = await userManager.FindByNameAsync(loginViewModel.UserName);
                if (user != null)
                {
                    await signManager.SignOutAsync();
                    Microsoft.AspNetCore.Identity.SignInResult signInResult = await signManager.PasswordSignInAsync(user, loginViewModel.Password, false, false);
                    if (signInResult.Succeeded)
                    {
                        AppUser loggingUser = await userManager.FindByNameAsync(loginViewModel.UserName);
                        if(await userManager.IsInRoleAsync(loggingUser, "Admins"))
                        {
                            return Redirect("/users");
                        }
                        return Redirect(loginViewModel?.ReturnUrl ?? "/profile");
                    }
                }
            }
            ModelState.AddModelError("", "Incorrect Username or Password");
            return View(loginViewModel);
        }

        [AllowAnonymous]
        public ViewResult SignUp()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> SignUp(SignUpViewModel signUpViewModel)
        {
            if (ModelState.IsValid)
            {
                AppUser user = new AppUser
                {
                    UserName = signUpViewModel.UserName,
                    Email= signUpViewModel.Email,
                    FirstName= signUpViewModel.FirstName,
                    LastName=signUpViewModel.LastName
                    
                };
                IdentityResult result = await userManager.CreateAsync(user, signUpViewModel.Password);
                if (result.Succeeded)
                {
                    await signManager.PasswordSignInAsync(signUpViewModel.UserName, signUpViewModel.Password,false,false);
                    return RedirectToAction(nameof(BlogController.MyPage), nameof(BlogController));
                }
                else
                {
                    foreach(IdentityError error in result.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }
                }
            }
            return View(signUpViewModel);
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await signManager.SignOutAsync();
            return Redirect("/");
        }

        [AllowAnonymous]
        public IActionResult AccessDenied()
        {
            return View();
        }

    }
}
