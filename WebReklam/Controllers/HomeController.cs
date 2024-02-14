using Infrastructure_WebReklam.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using WebReklam.Models;

namespace WebReklam.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IVillageRepository _villageRepository;

        public HomeController(ILogger<HomeController> logger, IVillageRepository villageRepository)
        {
            _logger = logger;
            _villageRepository = villageRepository;
        }

        public IActionResult Index()
        {
            return View();
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
