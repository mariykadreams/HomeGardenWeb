using HomeGardenWeb.Models;
using HomeGardenWeb.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace HomeGardenWeb.Controllers
{
    public class PlantsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PlantsController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var plants = _context.Plants.Include(p => p.Category).ToList();
            return View(plants);
        }

        public IActionResult Create()
        {
            var categories = _context.Category.Select(c => new
            {
                c.category_id,
                c.category_name
            }).ToList();

            if (categories == null || !categories.Any())
            {
                ModelState.AddModelError(string.Empty, "No categories available.");
                return View();
            }

            ViewBag.Categories = new SelectList(categories, "category_id", "category_name");

            return View();
        }



        [HttpPost]
        public IActionResult Create(PlantsDto plantDto)
        {
            if (!ModelState.IsValid)
            {
                var categories = _context.Category.Select(c => new
                {
                    c.category_id,
                    c.category_name
                }).ToList();

                ViewBag.Categories = new SelectList(categories, "category_id", "category_name");

                return View(plantDto);
            }

            var plant = new Plants
            {
                name = plantDto.Name, 
                description = plantDto.Description,  
                price = plantDto.Price, 
                category_id = plantDto.CategoryId 
            };

            _context.Plants.Add(plant);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }

        public IActionResult Edit(int id)
        {
            var plant = _context.Plants.Find(id);
            if (plant == null)
            {
                return RedirectToAction("Index");
            }

            var categories = _context.Category.Select(c => new
            {
                c.category_id,
                c.category_name
            }).ToList();

            ViewBag.Categories = new SelectList(categories, "category_id", "category_name", plant.category_id);

            var plantDto = new PlantsDto
            {
                Name = plant.name, 
                Description = plant.description,  
                Price = plant.price, 
                CategoryId = plant.category_id 
            };

            return View(plantDto);
        }


        [HttpPost]
        public IActionResult Edit(int id, PlantsDto plantDto)
        {
            if (!ModelState.IsValid)
            {
                var categories = _context.Category.Select(c => new
                {
                    c.category_id,
                    c.category_name
                }).ToList();

                ViewBag.Categories = new SelectList(categories, "category_id", "category_name", plantDto.CategoryId);

                return View(plantDto);
            }

            var plant = _context.Plants.Find(id);
            if (plant == null)
            {
                return RedirectToAction("Index");
            }

            plant.name = plantDto.Name; 
            plant.description = plantDto.Description; 
            plant.price = plantDto.Price; 
            plant.category_id = plantDto.CategoryId; 

            _context.SaveChanges();
            return RedirectToAction("Index");
        }


        [HttpPost]
        public IActionResult Delete(int id)
        {
            var plant = _context.Plants.Find(id);
            if (plant == null)
            {
                return Json(new { success = false, message = "Plant not found" });
            }

            _context.Plants.Remove(plant);
            _context.SaveChanges();

            return Json(new { success = true, message = "Plant has been deleted" });
        }


    }
}
