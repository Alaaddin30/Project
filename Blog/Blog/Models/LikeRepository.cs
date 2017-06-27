using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlogApp.Models
{
    public class LikeRepository : ILikeRepository
    {
        private AppDbContext context;
        public LikeRepository(AppDbContext _context)
        {
            context = _context;
        }

        public IEnumerable<Like> Likes => context.Likes;

        public IEnumerable<Like> LikesByBlogId(int blogId)
        {
            return context.Likes.Where(l => l.BlogId == blogId);
        }

        public void AddLike(Like like)
        {
            context.Likes.Add(like);
            context.SaveChanges();
        }
    }
}
