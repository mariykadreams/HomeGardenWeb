using HomeGardenWeb.Models;
using HomeGardenWeb.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Diagnostics;
using System.Linq;

namespace HomeGardenWeb.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _context;

        public HomeController(ILogger<HomeController> logger, ApplicationDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index()
        {
            var plants = _context.Plants.Include(p => p.Category).ToList();
            return View(plants);
        }

        public IActionResult Details(int id)
        {
            
            var plant = _context.Plants
                .Include(p => p.Category) 
                .Include(p => p.WateringFrequencies) 
                .FirstOrDefault(p => p.plant_id == id);

            if (plant == null)
            {
                return NotFound();
            }

            return View(plant);
        }




        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
