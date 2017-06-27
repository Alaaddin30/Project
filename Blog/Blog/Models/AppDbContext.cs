using BlogApp.Models.EntityConfiguration;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlogApp.Models
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
        public DbSet<Blog> Blogs { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Like> Likes { get; set; }
        public DbSet<Comment> Comments { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            CategoryConfiguration.ConfigureCategory(modelBuilder);
            BlogConfiguration.ConfigureBlog(modelBuilder);
            LikeConfiguration.ConfigureLike(modelBuilder);
            CommentConfiguration.ConfigureComment(modelBuilder);
        }
    }
}
