using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlogApp.Models.EntityConfiguration
{
    public class CommentConfiguration
    {
        public static void ConfigureComment(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Comment>()
                .HasKey(c=>c.CommentId);
        }
    }
}
