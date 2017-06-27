using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlogApp.Infrastructure
{
    public static class CurrentHttpContext
    {
        static IHttpContextAccessor httpContextAccessor;
        public static void Configure(IApplicationBuilder app)
        {
            // Enable IHttpContextService in Startup.ConfigureServices
            httpContextAccessor = app.ApplicationServices.GetRequiredService<IHttpContextAccessor>();
        }
        public static HttpContext Current { get { return httpContextAccessor?.HttpContext; } }
    }
}
