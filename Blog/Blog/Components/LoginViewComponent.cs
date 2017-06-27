using BlogApp.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewComponents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlogApp.Components
{
    public class LoginViewComponent:ViewComponent
    {
        private UserManager<AppUser> userManager;
        public LoginViewComponent(UserManager<AppUser> _userManager)
        {
            userManager = _userManager;
        }
        public ViewViewComponentResult Invoke()
        {
            string username = userManager.GetUserName(HttpContext.User);
            ViewBag.Username = username;
            return View();
        }
    }
}
