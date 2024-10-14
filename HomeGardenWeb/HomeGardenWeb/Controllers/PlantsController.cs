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

            try
            {
                var plant = new Plants
                {
                    name = plantDto.Name,
                    description = plantDto.Description,
                    price = plantDto.Price,
                    category_id = plantDto.CategoryId
                };

                _context.Plants.Add(plant);
                _context.SaveChanges();  

                var wateringFrequency = new WateringFrequency
                {
                    frequency_name = plantDto.FrequencyName,
                    water_volume = plantDto.WaterVolume,
                    watering_interval_days = plantDto.WateringIntervalDays,
                    notes = plantDto.Notes,
                    plant_id = plant.plant_id 
                };

                _context.WateringFrequency.Add(wateringFrequency);
                _context.SaveChanges();

                return RedirectToAction("Index");  
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);  
                return View(plantDto);
            }
        }

        public IActionResult Edit(int id)
        {
            var plant = _context.Plants
                .Include(p => p.WateringFrequencies) 
                .FirstOrDefault(p => p.plant_id == id);

            if (plant == null)
            {
                return RedirectToAction("Index");
            }

            var wateringFrequency = plant.WateringFrequencies?.FirstOrDefault() ?? new WateringFrequency();

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
                CategoryId = plant.category_id,
                FrequencyName = wateringFrequency.frequency_name,
                WaterVolume = wateringFrequency.water_volume,
                WateringIntervalDays = wateringFrequency.watering_interval_days,
                Notes = wateringFrequency.notes
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

            var plant = _context.Plants
                .Include(p => p.WateringFrequencies)
                .FirstOrDefault(p => p.plant_id == id);

            if (plant == null)
            {
                return RedirectToAction("Index");
            }

            plant.name = plantDto.Name;
            plant.description = plantDto.Description;
            plant.price = plantDto.Price;
            plant.category_id = plantDto.CategoryId;

            var wateringFrequency = plant.WateringFrequencies?.FirstOrDefault();
            if (wateringFrequency != null)
            {
                wateringFrequency.frequency_name = plantDto.FrequencyName;
                wateringFrequency.water_volume = plantDto.WaterVolume;
                wateringFrequency.watering_interval_days = plantDto.WateringIntervalDays;
                wateringFrequency.notes = plantDto.Notes;
            }
            else
            {
                wateringFrequency = new WateringFrequency
                {
                    frequency_name = plantDto.FrequencyName,
                    water_volume = plantDto.WaterVolume,
                    watering_interval_days = plantDto.WateringIntervalDays,
                    notes = plantDto.Notes,
                    plant_id = plant.plant_id
                };
                _context.WateringFrequency.Add(wateringFrequency);
            }

            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult Delete(int id)
        {
            var plant = _context.Plants
                .Include(p => p.WateringFrequencies) 
                .FirstOrDefault(p => p.plant_id == id);

            if (plant == null)
            {
                return Json(new { success = false, message = "Plant not found" });
            }

            if (plant.WateringFrequencies != null && plant.WateringFrequencies.Any())
            {
                _context.WateringFrequency.RemoveRange(plant.WateringFrequencies);
            }

            _context.Plants.Remove(plant);
            _context.SaveChanges();

            return Json(new { success = true});
        }



    }
}
