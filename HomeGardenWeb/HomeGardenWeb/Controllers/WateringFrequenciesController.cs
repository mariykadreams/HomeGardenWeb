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

    }
}
