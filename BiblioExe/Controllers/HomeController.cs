using BiblioExe.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace BiblioExe.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ContextoBD _context;
        public HomeController(ILogger<HomeController> logger, ContextoBD context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacidad()
        {
            return View();
        }

        public IActionResult AcercaDe()
        {
            return View();
        }

        public IActionResult TerminosYCondiciones()
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
