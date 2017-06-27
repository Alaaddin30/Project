using BlogApp.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlogApp.TagHelpers
{
    [HtmlTargetElement(Attributes ="user-info")]
    public class UserInfoTagHelper:TagHelper
    {
        private UserManager<AppUser> userManager;
        public UserInfoTagHelper(UserManager<AppUser> _userManager)
        {
            userManager = _userManager;
        }

        [HtmlAttributeName("user-name-id")]
        public string UserId { get; set; }

        [HtmlAttributeName("user-name-cssClass")]
        public string CssClass { get; set; }

        [HtmlAttributeName("user-info")]
        public UserInfoEnum UserInfo { get; set; }
        public async override Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            //return base.ProcessAsync(context, output);
            TagBuilder span = new TagBuilder("span");
            span.AddCssClass(CssClass);
            AppUser user = await userManager.FindByIdAsync(UserId);
            switch (UserInfo)
            {
                case UserInfoEnum.USERNAME:
                    span.InnerHtml.Append(user.UserName);
                    break;
                case UserInfoEnum.EMAIL:
                    span.InnerHtml.Append(user.Email);
                    break;
                case UserInfoEnum.FISRTNAME:
                    span.InnerHtml.Append(user.FirstName);
                    break;
                case UserInfoEnum.LASTNAME:
                    span.InnerHtml.Append(user.LastName);
                    break;
                default:
                    span.InnerHtml.Append(user.UserName);
                    break;

            }
            output.Content.AppendHtml(span);
        }
    }
}
