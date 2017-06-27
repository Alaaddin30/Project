using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlogApp.Models
{
    public class CategoryRepository:ICategoryRepository
    {
        private AppDbContext context;
        public CategoryRepository(AppDbContext _context)
        {
            context = _context;
        }

        public IEnumerable<Category> Categories => context.Categories;

        public void Add(Category _cattegory)
        {
            context.Categories.Add(_cattegory);
            context.SaveChanges();
        }


        public int Delete(int id)
        {
            Category entityToDelete = context.Categories
                .Include(c => c.Blogs)
                .FirstOrDefault(c => c.CategoryId == id);
            if (entityToDelete != null)
            {
                context.Categories.Remove(entityToDelete);
                return context.SaveChanges();
            }
            return 0;
        }

        public void Update(Category _category)
        {
            context.Categories.Update(_category);
            context.SaveChanges();
        }
    }
}
