using HomeGardenWeb.Models;
using HomeGardenWeb.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace HomeGardenWeb.Controllers
{
    public class WateringFrequenciesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public WateringFrequenciesController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var wateringFrequencies = _context.WateringFrequency.Include(w => w.Plant).ToList();
            return View(wateringFrequencies);
        }

        public IActionResult Create()
        {
            var plants = _context.Plants.Select(p => new
            {
                p.plant_id,
                p.name
            }).ToList();

            if (plants == null || !plants.Any())
            {
                ModelState.AddModelError(string.Empty, "No plants available.");
                return View();
            }

            ViewBag.Plants = new SelectList(plants, "plant_id", "name");
            return View();
        }

        [HttpPost]
        public IActionResult Create(WateringFrequencyDto wateringFrequencyDto)
        {
            if (!ModelState.IsValid)
            {
                var plants = _context.Plants.Select(p => new
                {
                    p.plant_id,
                    p.name
                }).ToList();

                ViewBag.Plants = new SelectList(plants, "plant_id", "name");

                return View(wateringFrequencyDto);
            }

            var wateringFrequency = new WateringFrequency
            {
                frequency_name = wateringFrequencyDto.FrequencyName,
                water_volume = wateringFrequencyDto.WaterVolume,
                watering_interval_days = wateringFrequencyDto.WateringIntervalDays,
                notes = wateringFrequencyDto.Notes,
                plant_id = wateringFrequencyDto.PlantId
            };

            _context.WateringFrequency.Add(wateringFrequency);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }

        public IActionResult Edit(int id)
        {
            var wateringFrequency = _context.WateringFrequency.Find(id);
            if (wateringFrequency == null)
            {
                return RedirectToAction("Index");
            }

            var plants = _context.Plants.Select(p => new
            {
                p.plant_id,
                p.name
            }).ToList();

            ViewBag.Plants = new SelectList(plants, "plant_id", "name", wateringFrequency.plant_id);

            var wateringFrequencyDto = new WateringFrequencyDto
            {
                FrequencyName = wateringFrequency.frequency_name,
                WaterVolume = wateringFrequency.water_volume,
                WateringIntervalDays = wateringFrequency.watering_interval_days,
                Notes = wateringFrequency.notes,
                PlantId = wateringFrequency.plant_id
            };

            return View(wateringFrequencyDto);
        }

        [HttpPost]
        public IActionResult Edit(int id, WateringFrequencyDto wateringFrequencyDto)
        {
            if (!ModelState.IsValid)
            {
                var plants = _context.Plants.Select(p => new
                {
                    p.plant_id,
                    p.name
                }).ToList();

                ViewBag.Plants = new SelectList(plants, "plant_id", "name", wateringFrequencyDto.PlantId);

                return View(wateringFrequencyDto);
            }

            var wateringFrequency = _context.WateringFrequency.Find(id);
            if (wateringFrequency == null)
            {
                return RedirectToAction("Index");
            }

            wateringFrequency.frequency_name = wateringFrequencyDto.FrequencyName;
            wateringFrequency.water_volume = wateringFrequencyDto.WaterVolume;
            wateringFrequency.watering_interval_days = wateringFrequencyDto.WateringIntervalDays;
            wateringFrequency.notes = wateringFrequencyDto.Notes;
            wateringFrequency.plant_id = wateringFrequencyDto.PlantId;

            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult Delete(int id)
        {
            var wateringFrequency = _context.WateringFrequency.Find(id);
            if (wateringFrequency == null)
            {
                return NotFound();
            }

            _context.WateringFrequency.Remove(wateringFrequency);
            _context.SaveChanges();

            return Ok();
        }
    }
}
