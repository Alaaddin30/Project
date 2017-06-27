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
    }
}
