using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlogApp.Models.Dtos
{
    public class CommentDto
    {
        public int BlogId { get; set; }
        public string Body { get; set; }
    }
}
