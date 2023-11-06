using Microsoft.AspNetCore.Mvc;
using NoteApp.Models;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Security.Principal;
using System.Text;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

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

		private NoteViewModel _noteViewModel { get; set; } = new();

		private List<SelectListItem> _notesSelectListItems { get; set; } = new();

		[HttpGet]
		public IActionResult Index()
		{
			_noteViewModel.NotesSelectListItems = GetNotesSelectListItems();

			return View(_noteViewModel);
		}

		[HttpPost]
		public IActionResult Index(NoteViewModel noteViewModel)
		{
			var selectedNote = _noteViewModel.NoteViewModelList.FirstOrDefault(c => c.Content == noteViewModel.Title);
			_noteViewModel.CurrentNote = selectedNote;

			_noteViewModel.NotesSelectListItems = GetNotesSelectListItems();
			return View(_noteViewModel);
		}

		public IActionResult About()
		{
			return View();
		}

		public IActionResult AddNote()
        {
	        IsEditing = true;

	        var note = new NoteViewModel();
	        _noteViewModel.NoteViewModelList.Add(note);

			_noteViewModel.NotesSelectListItems = GetNotesSelectListItems();
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

		[HttpPost]
        public IActionResult AcceptChanges(NoteViewModel noteViewModel)
        {
	        IsEditing = false;

	        _noteViewModel.NoteViewModelList.Add(noteViewModel);
	        _noteViewModel.CurrentNote = noteViewModel;

	        _noteViewModel.NotesSelectListItems = GetNotesSelectListItems();
			return View("Index", _noteViewModel);
        }

        public IActionResult CancelChanges()
        {
	        IsEditing = false;
			return View("Index");
		}

        private List<SelectListItem> GetNotesSelectListItems()
        {
	        var notesSelectListItems = new List<SelectListItem>();

	        foreach (var note in _noteViewModel.NoteViewModelList)
	        {
		        var selectList = new SelectListItem()
		        {
			        Text = note.Title,
			        Value = note.Content,
					Selected = false
		        };

		        notesSelectListItems.Add(selectList);
	        }

			return notesSelectListItems;
        }
	}
}