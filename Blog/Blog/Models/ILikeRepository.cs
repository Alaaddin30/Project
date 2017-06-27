using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlogApp.Models
{
    public interface ILikeRepository
    {
        IEnumerable<Like> Likes { get; }
        IEnumerable<Like> LikesByBlogId(int blogId);
        void AddLike(Like like);
    }
}
