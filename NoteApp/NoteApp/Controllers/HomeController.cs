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

		public IActionResult Index()
        {
            var note = new Note();
	        return View(note);
        }

        public IActionResult About()
        {
	        return View();
        }

        [HttpGet]
        public ActionResult AddNote()
        {
	        return Empty;
        }
	}
}