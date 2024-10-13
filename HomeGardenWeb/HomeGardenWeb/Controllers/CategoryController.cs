using HomeGardenWeb.Models;
using HomeGardenWeb.Services;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Headers;
using System.Reflection.Metadata;

namespace HomeGardenWeb.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment environment;
        public CategoryController(ApplicationDbContext context, IWebHostEnvironment environment)
        {
            this._context = context;
            this.environment = environment;
        }

        public IActionResult Index()
        {
            var category = _context.Category.ToList();   
            return View(category);
        }

        public IActionResult Create()
        {
           
            return View();
        }

        [HttpPost]
        public IActionResult Create(CategoryDto categoryDto)
        {
            if(categoryDto.category_name == null) 
            {
                ModelState.AddModelError("category_name", "Name is required");
            }
            
            if(!ModelState.IsValid)
            {
                return View(categoryDto);
            }

            Category category = new Category()
            {
                category_name = categoryDto.category_name,
            };

            _context.Category.Add(category); 
            _context.SaveChanges();

            return RedirectToAction("Index", "Category");

        }

        [HttpPost]
        public IActionResult Edit(int id, CategoryDto categoryDto)
        {
            var category = _context.Category.Find(id);

            if (categoryDto.category_name == null)
            {
                ModelState.AddModelError("category_name", "Name is required");
            }

            if (!ModelState.IsValid)
            {
                return View(categoryDto);
            }

            if (category == null)
            {
                return RedirectToAction("Index", "Category");
            }

            category.category_name = categoryDto.category_name;

            _context.SaveChanges();

            return RedirectToAction("Index", "Category");
        }

        public IActionResult Edit(int id)
        {
            var category = _context.Category.Find(id);

            if (category == null)
            {
                return RedirectToAction("Index", "Category");
            }

            var categoryDto = new CategoryDto()
            {  
                category_name = category.category_name
            };

            ViewData["category_id"] = category.category_id;
            ViewData["category_name"] = category.category_name;


            return View(categoryDto);
        }

        public IActionResult Delete(int id)
        {
            var category = _context.Category.Find(id);

            if (category == null)
            {
                return RedirectToAction("Index", "Category");
            }

            try
            {
                _context.Category.Remove(category);
                _context.SaveChanges();
                return Json(new { success = true });
            }
            catch (Microsoft.EntityFrameworkCore.DbUpdateException)
            {
                return Json(new { success = false, message = "This category cannot be deleted because it is linked to a plant." });
            }
        }

    }
}
