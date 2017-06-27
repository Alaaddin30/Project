using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlogApp.Models
{
    public static class SeedDatabase
    {
        public static void SeedCategory(IApplicationBuilder app)
        {
            AppDbContext context = app.ApplicationServices.GetRequiredService<AppDbContext>();
            if (!context.Categories.Any())
            {
                context.Categories.AddRange(new Category[]{
                    new Category(){CategoryName="Category-1"},
                    new Category(){CategoryName="Category-2"},
                    new Category(){CategoryName="Category-3"},
                    new Category(){CategoryName="Category-4"},
                    new Category(){CategoryName="Category-5"}
                });
                context.SaveChanges();
            }
        }
        public static void SeedBlog(IApplicationBuilder app)
        {
            AppDbContext context = app.ApplicationServices.GetRequiredService<AppDbContext>();
            if (!context.Blogs.Any())
            {
                context.Blogs.AddRange(InitiaData.BlogList);
                context.SaveChanges();
            }
        }

        public static async void SeedIdentity(IApplicationBuilder app)
        {
            string userName = "Admin";
            string password = "Admin";
            string role = "Admins";
            UserManager<AppUser> userManager = app.ApplicationServices.GetRequiredService<UserManager<AppUser>>();
            RoleManager<IdentityRole> roleManager = app.ApplicationServices.GetRequiredService<RoleManager<IdentityRole>>();
            AppUser user = await userManager.FindByNameAsync(userName);
            if (user == null)
            {
                if(await roleManager.FindByNameAsync("Admins") == null)
                {
                    await roleManager.CreateAsync(new IdentityRole { Name = role });
                }
                user = new AppUser() { UserName = userName, Email="Admin@example.com", FirstName = "Admin", LastName = "Admin"};
                IdentityResult result= await userManager.CreateAsync(user, password);
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(user, role);
                }
            }
        }
    }
}
