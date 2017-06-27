using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using BlogApp.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using BlogApp.Infrastructure;

namespace BlogApp
{
    public class Startup
    {
        IConfigurationRoot Configuration;
        public Startup(IHostingEnvironment env)
        {
            Configuration = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json").Build();
        }
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<AppDbContext>(options => options.UseSqlServer(Configuration.GetConnectionString("BlogConnection")));
            services.AddDbContext<AppIdentityDbContext>(options => options.UseSqlServer(Configuration.GetConnectionString("BlogIdentityConnection")));
            services.AddIdentity<AppUser, IdentityRole>(options=>
            {
                options.Password.RequiredLength = 5;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireUppercase = false;
                options.Password.RequireDigit = false;
                options.User.RequireUniqueEmail = true;
                options.User.AllowedUserNameCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ";
                options.Cookies.ApplicationCookie.LoginPath = "/user/in";
                options.Cookies.ApplicationCookie.AccessDeniedPath = "/denied";
            }).AddEntityFrameworkStores<AppIdentityDbContext>();
            services.AddTransient<IBlogRepository, BlogRepository>();
            services.AddTransient<ICategoryRepository, CategoryRepository>();
            services.AddTransient<ILikeRepository, LikeRepository>();
            services.AddTransient<ICommentRepository, CommentRepository>();
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddMvc();
        }

        public void Configure(IApplicationBuilder app)
        {
            app.UseDeveloperExceptionPage();
            app.UseStatusCodePages();
            app.UseStaticFiles();
            app.UseIdentity();
            app.UseMvc(routes=>{
                routes.MapRoute(
                name: null,
                template: "{categoryId:int}/{page:int}",
                defaults: new { controller = "Blog", action = "Index"}
                );
                routes.MapRoute(
                name: null,
                template: "{page:int}",
                defaults: new { controller = "Blog", action = "Index", page=1 }
                );
                routes.MapRoute(
                name: null,
                template: "{categoryId:int}",
                defaults: new { controller = "Blog", action = "Index", page = 1 }
                );
                routes.MapRoute(
                name: null,
                template: "user/new",
                defaults: new { controller = "Account", action = "SignUp" }
                );
                routes.MapRoute(
                name: null,
                template: "user/in",
                defaults: new { controller = "Account", action = "Login" }
                );
                routes.MapRoute(
                name: null,
                template: "user/out",
                defaults: new { controller = "Account", action = "Logout" }
                );
                routes.MapRoute(
                name: null,
                template: "user",
                defaults: new { controller = "Account", action = "Index" }
                );
                routes.MapRoute(
                 name: null,
                 template: "blogs",
                 defaults: new { controller = "Blog", action = "Index" }
                 );
                routes.MapRoute(
                name: null,
                template: "blog/{blogId:int}",
                defaults: new { controller = "Blog", action = "ViewBlog" }
                );
                routes.MapRoute(
                name: null,
                template: "users",
                defaults: new { controller = "UserAdmin", action = "Index" }
                );
                routes.MapRoute(
                name: null,
                template: "users/new",
                defaults: new { controller = "UserAdmin", action = "Create" }
                );
                routes.MapRoute(
                name: null,
                template: "users/delete/{id:required}",
                defaults: new { controller = "UserAdmin", action = "Delete"}
                );
                routes.MapRoute(
                name: null,
                template: "users/edit/{id:required}",
                defaults: new { controller = "UserAdmin", action = "Edit" }
                );
                routes.MapRoute(
                name: null,
                template: "roles",
                defaults: new { controller = "RoleAdmin", action = "Index" }
                );
                routes.MapRoute(
                name: null,
                template: "roles/new",
                defaults: new { controller = "RoleAdmin", action = "Create" }
                );
                routes.MapRoute(
                name: null,
                template: "roles/remove/{id:required}",
                defaults: new { controller = "RoleAdmin", action = "RemoveFromRole" }
                );
                routes.MapRoute(
                name: null,
                template: "roles/add/{id:required}",
                defaults: new { controller = "RoleAdmin", action = "AddToRole" }
                );
                routes.MapRoute(
                name: null,
                template: "roles/trash/{id:required}",
                defaults: new { controller = "RoleAdmin", action = "Delete" }
                );
                routes.MapRoute(
                name: null,
                template: "denied",
                defaults: new { controller = "Account", action = "AccessDenied" }
                );
                routes.MapRoute(
                name: null,
                template: "",
                defaults: new { controller = "Blog", action = "Index" }
                );
            });

            CurrentHttpContext.Configure(app);
            SeedDatabase.SeedCategory(app);
            SeedDatabase.SeedBlog(app);
            SeedDatabase.SeedIdentity(app);
        }
    }
}
