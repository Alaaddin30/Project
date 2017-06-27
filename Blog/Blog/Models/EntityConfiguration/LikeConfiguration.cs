using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlogApp.Models.EntityConfiguration
{
    public static class LikeConfiguration
    {
        public static void ConfigureLike(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Like>()
                .HasKey(l => new { l.UserId, l.BlogId });
        }
    }
}
