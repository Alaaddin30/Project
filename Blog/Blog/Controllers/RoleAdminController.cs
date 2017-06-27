using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using BlogApp.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using BlogApp.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;

namespace BlogApp.Controllers
{
    [Authorize(Roles ="Admins")]
    public class RoleAdminController : Controller
    {
        private RoleManager<IdentityRole> roleManager;
        private UserManager<AppUser> userManager;
        public RoleAdminController(RoleManager<IdentityRole> _roleManager, UserManager<AppUser> _userManager)
        {
            roleManager = _roleManager;
            userManager = _userManager;
        }
        public IActionResult Index() => View(roleManager.Roles);

        [HttpGet]
        public ViewResult Create() => View();
        [HttpPost]
        public async Task<IActionResult> Create(CreateRoleViewModel model)
        {
            if (ModelState.IsValid)
            {
                IdentityResult result = await roleManager.CreateAsync(new IdentityRole { Name = model.Name });
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
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(string id)
        {
            IdentityRole role = await roleManager.FindByIdAsync(id);
            if (role != null)
            {
                IdentityResult result = await roleManager.DeleteAsync(role);
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
            return View(nameof(Index), roleManager.Roles);
        }

        [HttpGet]
        public async Task<IActionResult> RemoveFromRole(string id)
        {
            RoleMembers members= await UsersByRole(id);
            return View(new EditUsersRole()
            {
                Users=members.Memebrs,
                Role= members.Role
            });
        }

        [HttpPost]
        public async Task<IActionResult> RemoveFromRole(EditUsersRoleViewModel model)
        {
            if(ModelState.IsValid && model.Ids != null)
            {
                foreach(string id in model.Ids)
                {
                    AppUser user = await userManager.FindByIdAsync(id);
                    if (user != null)
                    {
                        IdentityResult result = await userManager.RemoveFromRoleAsync(user, model.Name);
                        if (!result.Succeeded)
                        {
                            foreach (IdentityError err in result.Errors)
                            {
                                ModelState.AddModelError("", err.Description);
                            }
                        }
                    }
                    
                }
            }
            if (ModelState.IsValid)
                return RedirectToAction(nameof(Index));
            return await RemoveFromRole(model.RoleId);
        }

        [HttpGet]
        public async Task<IActionResult> AddToRole(string id)
        {
            RoleMembers  nonMembers= await UsersByRole(id);
            return View(new EditUsersRole()
            {
                Users = nonMembers.NonMembers,
                Role = nonMembers.Role
            });
        }



        [HttpPost]
        public async Task<IActionResult> AddToRole(EditUsersRoleViewModel model)
        {
            if (ModelState.IsValid && model.Ids != null)
            {
                foreach (string id in model.Ids)
                {
                    AppUser user = await userManager.FindByIdAsync(id);
                    if (user != null)
                    {
                        IdentityResult result = await userManager.AddToRoleAsync(user, model.Name);
                        if (!result.Succeeded)
                        {
                            foreach (IdentityError err in result.Errors)
                            {
                                ModelState.AddModelError("", err.Description);
                            }
                        }
                    }

                }
            }
            if (ModelState.IsValid)
                return RedirectToAction(nameof(Index));
            return await AddToRole(model.RoleId);
        }



        private async Task<RoleMembers> UsersByRole(string roleId)
        {
            List<AppUser> members = new List<AppUser>();
            List<AppUser> nonMembers = new List<AppUser>();
            IdentityRole role = await roleManager.FindByIdAsync(roleId);
            if (role != null)
            {
                foreach (AppUser user in userManager.Users)
                {
                    if (user != null && await userManager.IsInRoleAsync(user, role.Name))
                    {
                        members.Add(user);
                    }
                    else
                    {
                        nonMembers.Add(user);
                    }
                }
            }
            return new RoleMembers()
            {
                Memebrs = members,
                NonMembers = nonMembers,
                Role=role
            };
        }
    }
}
