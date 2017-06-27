using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlogApp.Models
{
    public interface ICommentRepository
    {
        IEnumerable<Comment> Comments { get; }
        void AddComment(Comment comment);
    }
}
