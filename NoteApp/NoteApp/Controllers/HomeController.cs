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
	        return View(new Note());
        }

		public IActionResult About()
        {
	        return View();
        }

        public IActionResult AddNote()
        {
	        IsEditing = true;

	        return View("Index", new Note());
        }

        public IActionResult EditNote()
        {
	        IsEditing = true;

	        return View("Index", new Note());
        }

        public IActionResult DeleteNote()
        {
	        return View("Index", new Note());
		}

        public IActionResult AcceptChanges()
        {
	        IsEditing = false;
			return View("Index", new Note());
        }

        public IActionResult CancelChanges()
        {
	        IsEditing = false;
			return View("Index", new Note());
		}
	}
}