using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BlogApp.Models
{
    public class Blog
    {
        [Key]
        public int BlogId { get; set; }
        [Required(ErrorMessage ="Title is required.")]
        public string Title { get; set; }
        [Required(ErrorMessage = "Body is required.")]
        public string Body { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        public int? CategoryId { get; set; }
        public Category Category { get; set; }

        public ICollection<Like> Likes { get; set; }
        public ICollection<Comment> Comments { get; set; }
    }
}
