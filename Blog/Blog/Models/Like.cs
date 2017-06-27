using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlogApp.Models
{
    public class Like
    {
        public string UserId { get; set; }
        public int BlogId { get; set; }
        public bool Liked { get; set; }
        public DateTime LikedAt { get; set; } = DateTime.Now;
        public Blog Blog { get; set; }
    }
}
