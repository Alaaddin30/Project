using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using BlogApp.Models;
using BlogApp.Models.ViewModels;

namespace BlogApp.Controllers
{
    public class BlogController : Controller
    {
        private IBlogRepository repository;
        private ICommentRepository commentRepository;
        public int ItemsPerPage = 5;
        public BlogController(IBlogRepository _repository, ICommentRepository _commentRepo)
        {
            repository = _repository;
            commentRepository = _commentRepo;
        }
 
        public ViewResult Index(int categoryId, int page=1)

        {
            IEnumerable<Models.Blog> blogs = repository.Blogs
                .Where(b=>categoryId==0 || b.CategoryId == categoryId)
                .OrderBy(b => b.BlogId).
                Skip((page - 1) * ItemsPerPage)
                .Take(ItemsPerPage);

            PaginationInfo paginationInfo = new PaginationInfo()

            {
                CurrentPage = page,
                ItemsPerPage = ItemsPerPage,
                TotalItems = categoryId ==0 ? repository.Blogs.Count() : repository.Blogs.Where(b => b.CategoryId == categoryId).Count()
            };
            return View(new BlogIndexViewModel() { Blogs= blogs, PaginationInfo=paginationInfo, CurrentCategoryId=categoryId});
        }

        public IActionResult ViewBlog(int blogId)
      {
            Models.Blog blog = repository.Blog(blogId);
           
            if (blog != null)
            {
                return View(new ViewBlogViewModel()
                {
                    Blog = blog,
                    Comments = blog.Comments
                });
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
