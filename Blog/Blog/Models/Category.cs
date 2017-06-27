using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BlogApp.Models
{
    public class Category
    {
        public int CategoryId { get; set; }
        [Required(ErrorMessage ="Category name is required")]
        public string CategoryName { get; set; }
        public ICollection<Blog> Blogs { get; set; }
    }
}
