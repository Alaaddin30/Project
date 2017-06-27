using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlogApp.Models
{
    public class Comment
    {
        public int CommentId { get; set; }
        public int BlogId { get; set; }
        public string UserId { get; set; }
        public string Body { get;set; }
        public DateTime CreatedAt { get; set; }
        public Blog Blog { get; set; }
    }
}
