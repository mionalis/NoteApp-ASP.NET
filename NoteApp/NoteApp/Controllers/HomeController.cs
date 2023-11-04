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
		private List<Note>	_noteList = new()
		{
			new Note() { Id = 1, Title = "A", IsSelected = false },
			new Note() { Id = 2, Title = "B", IsSelected = false },
			new Note() { Id = 3, Title = "C", IsSelected = false },
		};

		private NoteViewModel _noteViewModel { get; set; } = new();

		[HttpGet]
		public IActionResult Index()
        {
	        _noteViewModel.ListBoxNotes = GetNotesForListBox();

	        return View(_noteViewModel);

		}

		public IActionResult About()
		{
			return View();
		}

		public IActionResult AddNote()
        {
	        IsEditing = true;

			var newNote = new Note();
			_noteList.Add(newNote);

			_noteViewModel.ListBoxNotes = GetNotesForListBox();

			return View("Index", _noteViewModel);
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
			        Value = note.Id.ToString(),
			        Selected = note.IsSelected
		        };

		        notesSelectListItems.Add(selectList);
		        _noteViewModel.CurrentNote = note;
	        }

	        return notesSelectListItems;
        }
	}
}