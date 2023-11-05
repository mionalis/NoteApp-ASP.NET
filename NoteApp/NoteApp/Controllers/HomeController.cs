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

		// Для тестирования
		private List<NoteViewModel>	_noteList = new()
		{
			new NoteViewModel() { Title = "A", Content = "ldldld" },
			new NoteViewModel() { Title = "B", Content = "bbfdb" },
			new NoteViewModel() { Title = "C", Content = "tetette" }
		};

		private NoteViewModel _noteViewModel { get; set; } = new();

		[HttpGet]
		public IActionResult Index()
		{
			var citiesSelectListItems = new List<SelectListItem>();

			foreach (var note in _noteList)
			{
				var selectList = new SelectListItem()
				{
					Text = note.Title,
					Value = note.Content
				};
				citiesSelectListItems.Add(selectList);
			}

			_noteViewModel.NoteSelectListItems = citiesSelectListItems;

			return View(_noteViewModel);
		}

		[HttpPost]
		public IActionResult Index(NoteViewModel noteViewModel)
		{
			var test = _noteList.FirstOrDefault(c => c.Content == noteViewModel.Title);
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

        private List<SelectListItem> GetNotesForListBox()
        {
	        var notesSelectListItems = new List<SelectListItem>();

	        foreach (var note in _noteList)
	        {
		        var selectList = new SelectListItem()
		        {
			        Text = note.Title,
			        Value = note.Content
		        };

		        notesSelectListItems.Add(selectList);
	        }

	        return notesSelectListItems;
        }
	}
}