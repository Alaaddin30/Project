using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlogApp.Models.Dtos
{
    public class LikeDto
    {
        public int BlogId { get; set; }
        public bool Liked { get; set; }
    }
}
