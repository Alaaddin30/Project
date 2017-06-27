using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlogApp.Models.EntityConfiguration
{
    public static class CategoryConfiguration
    {
        public static void ConfigureCategory(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>()
                .HasMany(c => c.Blogs)
                .WithOne(b => b.Category)
                .IsRequired(false)
                .HasForeignKey(b => b.CategoryId);
        }
    }
}
