using BlogApp.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Razor.TagHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlogApp.TagHelpers
{
    [HtmlTargetElement(Attributes ="role-users")]
    public class RoleUsersTagHelper:TagHelper
    {
        private RoleManager<IdentityRole> roleManager;
        private UserManager<AppUser> userManager;
        public RoleUsersTagHelper(RoleManager<IdentityRole> _roleManager, UserManager<AppUser> _userManager)
        {
            roleManager = _roleManager;
            userManager = _userManager;
        }

        [HtmlAttributeName("role-id")]
        public string RoleId { get; set; }
        [HtmlAttributeName("role-users")]
        public RoleUserEnum RoleUsers { get; set; }

        public async override Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            List<string> users = new List<string>();
            IdentityRole role = await roleManager.FindByIdAsync(RoleId);
            if (role != null)
            {
                foreach(AppUser user in userManager.Users)
                {
                    if(user != null)
                    {
                        if(await userManager.IsInRoleAsync(user, role.Name))
                        {
                            users.Add(user.UserName);
                        }
                    }
                }
            }
            TagBuilder span = new TagBuilder("span");
            //span.AddCssClass("text-danger");
            if (RoleUsers == RoleUserEnum.MEMBERS)
            {
                span.InnerHtml.Append(users.Count == 0 ? "No Members" : users.Count.ToString() + " User(s)");
                output.Content.AppendHtml(span);
            }
            else
            {
                 int allUsers= userManager.Users.Count();
                int nonMembers = allUsers - (users.Count);
                span.InnerHtml.Append(nonMembers== 0 ? "All Memebrs" : nonMembers.ToString() + " User(s)");
                output.Content.AppendHtml(span);
            }
        }
    }
}
