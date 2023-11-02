using Microsoft.AspNetCore.Mvc;
using NoteApp.Models;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Security.Principal;
using System.Text;
using Microsoft.AspNetCore.Mvc.Rendering;

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

        private List<Note> _noteList { get; set; } = new();

        private NoteViewModel _noteViewModel { get; set; } = new();

		public IActionResult Index()
        {
	        var citiesSelectListItems = new List<SelectListItem>();

	        foreach (var note in _noteList)
	        {
		        var selectList = new SelectListItem()
		        { 
			        Text = note.Title,
			        Value = note.Id.ToString(),
			        Selected = note.IsSelected
		        };

		        citiesSelectListItems.Add(selectList);
	        }

			_noteViewModel.Notes = citiesSelectListItems;

	        return View(_noteViewModel);
        }

		public IActionResult About()
		{
			return View();
		}

		public IActionResult AddNote()
        {
	        IsEditing = true;

	        return View("Index");
        }

        public IActionResult EditNote()
        {
	        IsEditing = true;

	        return View("Index");
        }

        public IActionResult DeleteNote()
        {
	        return View("Index");
		}

        public IActionResult AcceptChanges()
        {
	        IsEditing = false;
			return View("Index");
        }

        public IActionResult CancelChanges()
        {
	        IsEditing = false;
			return View("Index");
		}
	}
}