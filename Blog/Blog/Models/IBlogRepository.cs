using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlogApp.Models
{
    public interface IBlogRepository
    {
        IEnumerable<Blog> Blogs { get; }
        Blog Blog(int id);
        void Add(Blog blog);
        int Delete(int id);
    }
}
