using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlogApp.Models.ViewModels
{
    public class ViewBlogViewModel
    {
        public Blog Blog { get; set; }
        public IEnumerable<Comment> Comments { get; set; }
    }
}
