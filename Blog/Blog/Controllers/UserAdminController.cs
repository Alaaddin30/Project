using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using BlogApp.Models;
using BlogApp.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;

namespace BlogApp.Controllers
{
    [Authorize(Roles = "Admins")]
    public class UserAdminController : Controller
    {
        private UserManager<AppUser> userManager;
        private IUserValidator<AppUser> userValidator;
        private IPasswordValidator<AppUser> passwordValidator;
        private IPasswordHasher<AppUser> passwordHasher;
        public UserAdminController(UserManager<AppUser> _userManager,IUserValidator<AppUser> _userValidator,IPasswordValidator<AppUser> _passwordValidator, IPasswordHasher<AppUser> _passwordHasher) {
            userManager = _userManager;
            userValidator = _userValidator;
            passwordValidator = _passwordValidator;
            passwordHasher = _passwordHasher;
        }
        public ViewResult Index() => View(userManager.Users);
        [HttpGet]
        public ViewResult Create() => View();
        [HttpPost]
        public async Task<IActionResult> Create(CreateUserViewModel _user)
        {
            if (ModelState.IsValid)
            {
                AppUser user = new AppUser
                {
                    UserName = _user.Username,
                    Email = _user.Email,
                    FirstName = _user.FirstName,
                    LastName = _user.FirstName
                };
                IdentityResult result = await userManager.CreateAsync(user, _user.Password);
                if (result.Succeeded)
                {
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    foreach(IdentityError error in result.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }
                }
            }
            return View(_user);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(string Id)
        {
            AppUser user = await userManager.FindByIdAsync(Id);
            if (user != null)
            {
                IdentityResult result = await userManager.DeleteAsync(user);
                if (result.Succeeded)
                {
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    foreach(IdentityError err in result.Errors)
                    {
                        ModelState.AddModelError("", err.Description);
                    }
                }
            }
            else
            {
                ModelState.AddModelError("", "User Not Found");
            }
            return View(nameof(Index), userManager.Users);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(string id)
        {
            AppUser user = await userManager.FindByIdAsync(id);
            if (user != null)
            {
                return View(new EditUserViewModel()
                {
                    Id =user.Id,
                    Username=user.UserName,
                    Email= user.Email,
                    Password= user.PasswordHash,
                    FirstName= user.UserName,
                    LastName=user.LastName
                });
            }
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<IActionResult> Edit(EditUserViewModel _user)
        {
            AppUser user = await userManager.FindByIdAsync(_user.Id);
            if (user != null)
            {
                //validate Email
                user.Email = _user.Email;
                IdentityResult emailResult = await userValidator.ValidateAsync(userManager, user);
                if (!emailResult.Succeeded)
                {
                    foreach(IdentityError err in emailResult.Errors)
                    {
                        ModelState.AddModelError("", err.Description);
                    }
                }
                //validate Password
                IdentityResult passwordResult = null;
                if (!string.IsNullOrEmpty(_user.Password))
                {
                    passwordResult = await passwordValidator.ValidateAsync(userManager, user, _user.Password);
                    if (passwordResult.Succeeded)
                    {
                        user.PasswordHash = passwordHasher.HashPassword(user, _user.Password);
                    }
                    else
                    {
                        foreach(IdentityError err in passwordResult.Errors)
                        {
                            ModelState.AddModelError("", err.Description);
                        }
                    }
                }
                if((emailResult.Succeeded && passwordResult==null) ||(emailResult.Succeeded && _user.Password!=string.Empty  && passwordResult.Succeeded)){
                    IdentityResult result = await userManager.UpdateAsync(user);
                    if (result.Succeeded)
                    {
                        return RedirectToAction(nameof(Index));
                    }
                    else
                    {
                        foreach(IdentityError err in result.Errors)
                        {
                            ModelState.AddModelError("", err.Description);
                        }
                    }
                }

            }
            else
            {
                ModelState.AddModelError("", "User Not Found");
            }
            return View(user);
        }
    }
}
