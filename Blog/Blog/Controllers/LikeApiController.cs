using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using BlogApp.Models.Dtos;
using BlogApp.Models;
using BlogApp.Infrastructure;
using Microsoft.AspNetCore.Identity;

namespace BlogApp.Controllers
{
    [Route("api/[controller]")]
    public class LikeApiController : Controller
    {
        private ILikeRepository repository;
        private UserManager<AppUser> userManager;

        public LikeApiController(ILikeRepository _repository, UserManager<AppUser> _userManager)
        {
            repository = _repository;
            userManager = _userManager;
        }


        [HttpPost]
        public bool Post(LikeDto likeDto)
        {
            string userId = userManager.GetUserId(CurrentHttpContext.Current?.User);
            Like userLiked = repository.Likes.FirstOrDefault(l => l.UserId == userId && l.BlogId == likeDto.BlogId);
            if (userId != null && userLiked == null)
            {
                repository.AddLike(new Like { BlogId = likeDto.BlogId, UserId = userId, Liked = likeDto.Liked });
                return true;
            }
            return false;
        }
    }
}
