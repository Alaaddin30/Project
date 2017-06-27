using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlogApp.Models.EntityConfiguration
{
    public static class BlogConfiguration
    {
        public static void ConfigureBlog(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Blog>()
                .HasMany(b =>b.Likes)
                .WithOne(l=>l.Blog)
                .IsRequired(true);
            modelBuilder.Entity<Blog>()
                .HasMany(b => b.Comments)
                .WithOne(c => c.Blog)
                .IsRequired(true);
        }
    }
}
