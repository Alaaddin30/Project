using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using BlogApp.Models;
using BlogApp.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using BlogApp.Infrastructure;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BlogApp.Controllers
{
    public class BlogController : Controller
    {
        private IBlogRepository repository;
        private ICommentRepository commentRepository;
        private UserManager<AppUser> userManager;
        private ICategoryRepository categoryRepository;
        public int ItemsPerPage = 5;
        public BlogController(IBlogRepository _repository,
            ICommentRepository _commentRepo,
            UserManager<AppUser> _userManager,
            ICategoryRepository _categoryRepo)
        {
            repository = _repository;
            commentRepository = _commentRepo;
            userManager = _userManager;
            categoryRepository = _categoryRepo;
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

        [Authorize]
        public IActionResult MyPage(int page=1)
        {
            IEnumerable<Models.Blog> blogs = null;
            if (UserId != null)
            {
               blogs = repository.Blogs.Where(b => b.UserId == UserId);
            }
            else
            {
                blogs=Enumerable.Empty<Models.Blog>();
            }

            PaginationInfo info = new PaginationInfo
            {
                TotalItems = blogs.Count(),
                ItemsPerPage= ItemsPerPage,
                CurrentPage= page
            };
            return View( new AdminViewModel<Models.Blog>
            {
                Sequuence= blogs,
                PaginationInfo= info
            });
        }

        [Authorize]
        [HttpGet]
        public IActionResult Create()
        {
            ViewBag.Categories = new SelectList(categoryRepository.Categories,
                nameof(Category.CategoryId),
                nameof(Category.CategoryName));
            return View();
        }

        [Authorize]
        [HttpPost]
        public IActionResult Create(CreateBlogViewModel model)
        {
            if(ModelState.IsValid && UserId != null)
            {
                repository.Add(new Models.Blog
                {
                    UserId= UserId,
                    CategoryId= model.CategoryId,
                    Title= model.Title,
                    Body=model.Body,
                    CreatedAt= DateTime.Now
                    
                });
                TempData["msg"] = "Blog created successfully";
                return RedirectToAction(nameof(MyPage));
            }
            return View(model);
        }

        [HttpPost]
        [Authorize]
        public IActionResult Delete(int id)
        {
            if (id != 0)
            {
                int res = repository.Delete(id);
                TempData["msg"]= res > 0 ? "Blog Deleted Successfully" :  "Something went wrong";
            }
            return RedirectToAction(nameof(MyPage));
        }

        [Authorize]
        [HttpGet]
        public IActionResult Edit(int id)
        {
            Models.Blog blog = repository.Blog(id);
            if (blog != null)
            {
                ViewBag.Categories = new SelectList(categoryRepository.Categories,
                    nameof(Category.CategoryId),
                    nameof(Category.CategoryName));
                return View( new CreateBlogViewModel
                {
                    BlogId= blog.BlogId,
                    CategoryId = blog.CategoryId,
                    Title= blog.Title,
                    Body= blog.Body
                });
            }
            TempData["msg"] = "Blog not found";
            return RedirectToAction(nameof(MyPage));
        }

        [Authorize]
        [HttpPost]
        public IActionResult Edit(CreateBlogViewModel model)
        {
            if (ModelState.IsValid)
            {
                if (repository.Blog(model.BlogId)!=null)
                {
                    repository.Save(new Models.Blog
                    {
                        BlogId= model.BlogId,
                        CategoryId = model.CategoryId,
                        Title = model.Title,
                        Body= model.Body
                    });
                    TempData["msg"] = "Blog Updated successfully";
                    return RedirectToAction(nameof(MyPage));
                }
            }
            return View(model);
        }

        private string UserId
        {
            get
            {
                return userManager.GetUserId(CurrentHttpContext.Current.User);
            }
        }
    }
}
