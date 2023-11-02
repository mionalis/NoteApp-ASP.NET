using Microsoft.AspNetCore.Mvc;
using NoteApp.Models;
using System.Diagnostics;
using System.Security.Principal;

namespace NoteApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public static bool IsEditing { get; set; }

        public IActionResult Index()
        {
	        return View();
        }

		public IActionResult About()
        {
	        return View();
        }

        public IActionResult EditMode()
        {
	        IsEditing = !IsEditing;

	        return View("Index");
        }
	}
}