using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using BlogApp.Models;
using BlogApp.Models.Dtos;
using Microsoft.AspNetCore.Identity;
using BlogApp.Infrastructure;

namespace BlogApp.Controllers
{
    [Route("api/[controller]")]
    public class CommentApiController : Controller
    {
        private ICommentRepository repository;
        private UserManager<AppUser> userManager;
        public CommentApiController(ICommentRepository _repository, UserManager<AppUser> _userManager)
        {
            repository = _repository;
            userManager = _userManager;
        }

        [HttpPost]
        public Comment Post(CommentDto commentDto)
        {
            string userId = userManager.GetUserId(CurrentHttpContext.Current?.User);
            if (userId != null)
            {
                Comment comment = new Comment()
                {
                    BlogId = commentDto.BlogId,
                    Body = commentDto.Body,
                    CreatedAt = DateTime.Now,
                    UserId = userId
                };
                repository.AddComment(comment);
                return comment;
            }
            return null;
        }
    }
}
