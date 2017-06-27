using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using BlogApp.Models;
using BlogApp.Models.ViewModels;

namespace BlogApp.Controllers
{
    public class BlogAdminController : Controller
    {
        private IBlogRepository repository;
        public int ItemsPerPage = 5;
        public BlogAdminController(IBlogRepository _repository)
        {
            repository = _repository;
        }
        public IActionResult Index(int page=1)
        {
            IEnumerable<Models.Blog> blogs = repository.Blogs
                .OrderBy(b => b.BlogId)
                .Skip((page - 1) * ItemsPerPage)
                .Take(ItemsPerPage);
            PaginationInfo info = new PaginationInfo()

            {
                CurrentPage = page,
                ItemsPerPage = ItemsPerPage,
                TotalItems = repository.Blogs.Count()
            };
            return View(new AdminViewModel<Models.Blog>()
            {
                Sequuence= blogs,
                PaginationInfo= info
            });
        }


        [HttpPost]
        public IActionResult Delete(int id)
        {
            if (id!=0)
            {
                int res= repository.Delete(id);
                TempData["msg"] = res > 0 ? $"Blog with {id} ID was deleted successfully" : "Blog not found or Somthing went wrong";
                return RedirectToAction(nameof(Index), new { page = 1 });
            }
            return RedirectToAction(nameof(Index), new { page = 1 });
        }


        [HttpGet]
        public IActionResult Add() => View();
        [HttpPost]
        public IActionResult Add(Models.Blog blog)
        {
            return View();
        }
    }
}
