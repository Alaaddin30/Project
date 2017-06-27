using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using BlogApp.Models;
using Microsoft.AspNetCore.Authorization;
using System.ComponentModel.DataAnnotations;
using BlogApp.Models.ViewModels;

namespace BlogApp.Controllers
{
    [Authorize(Roles ="Admins")]
    public class CategoryAdminController : Controller
    {
        private ICategoryRepository repository;
        public CategoryAdminController(ICategoryRepository _repository)
        {
            repository = _repository;
        }
        public IActionResult Index()
        {
            return View( repository.Categories);
        }

        [HttpGet]
        public IActionResult Create() => View();

        [HttpPost]
        public IActionResult Create(Category model)
        {
            if (ModelState.IsValid)
            {
                repository.Add(new Category {CategoryName= model.CategoryName });
                TempData["msg"] = $" Category with Name {model.CategoryName}  created successfully";
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }

        [HttpPost]
        public IActionResult Delete(int id)
        {
            Category category = repository.Categories.FirstOrDefault(c => c.CategoryId == id);
            if (category != null)
            {
                repository.Delete(category);
                TempData["msg"] = $"{category.CategoryName} deleted successfully";
                return RedirectToAction(nameof(Index));
            }
            TempData["msg"] = "Category Not Found";
            return RedirectToAction(nameof(Index));

        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            Category category = repository.Categories.FirstOrDefault(c => c.CategoryId == id);
            if (category != null)
            {
                return View(category);
            }
            TempData["msg"] = $"Category not found";
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public IActionResult Edit(Category _category)
        {
            if (ModelState.IsValid)
            {
                Category category = repository.Categories.FirstOrDefault(c => c.CategoryId == _category.CategoryId);
                if (category != null)
                {
                    category.CategoryName = _category.CategoryName;
                    repository.Update(category);
                    return RedirectToAction(nameof(Index));
                }
            }
            return View(_category);
        }
    }
}
