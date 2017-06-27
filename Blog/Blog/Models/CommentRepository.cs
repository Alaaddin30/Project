using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlogApp.Models
{
    public class CommentRepository : ICommentRepository
    {
        private AppDbContext context;
        public CommentRepository(AppDbContext _context )
        {
            context = _context;
        }
        public IEnumerable<Comment> Comments => context.Comments;

        public void AddComment(Comment comment)
        {
            context.Comments.Add(comment);
            context.SaveChanges();
        }
    }
}
