using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlogApp.Models
{
    public class BlogRepository : IBlogRepository
    {
        private AppDbContext context;
        public BlogRepository(AppDbContext _context)
        {
            context = _context;
        }
        public IEnumerable<Blog> Blogs => context.Blogs;

        public Blog Blog(int blogId)
        { 
            //Blog blog = context.Blogs.FirstOrDefault(b => b.BlogId == blogId);
            Blog blog = context.Blogs.Find(blogId);
            if (blog != null)
            {
                context.Blogs.Where(b => b.BlogId == blogId).Include(b => b.Comments).ToList();
                return blog;
            }
            return null;
        }

        public int Delete(int id)
        {
            Models.Blog entitytoDelet = context.Blogs
                .Include(b => b.Likes)
                .Include(b => b.Comments)
                .FirstOrDefault(b => b.BlogId == id);
            if (entitytoDelet != null)
            {
                context.Blogs.Remove(entitytoDelet);
                return context.SaveChanges();
            }
            return 0;
        }
        public void Add(Blog blog)
        {
            context.Blogs.Add(blog);
            context.SaveChanges();
        }

        public void Save(Blog _blog)
        {
            Models.Blog blog = context.Blogs.FirstOrDefault(b => b.BlogId == _blog.BlogId);
            if (blog != null)
            {
                blog.CategoryId = _blog.CategoryId;
                blog.Title = _blog.Title;
                blog.Body = _blog.Body;
            }
            context.Entry(blog).State = EntityState.Modified;
            context.SaveChanges();
        }
    }
}

