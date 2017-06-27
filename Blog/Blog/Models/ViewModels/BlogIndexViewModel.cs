using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlogApp.Models.ViewModels
{
    public class BlogIndexViewModel
    {
        public IEnumerable<Blog> Blogs { get; set; }
        public int CurrentCategoryId { get; set; }
        public PaginationInfo PaginationInfo { get; set; }
    }
}
