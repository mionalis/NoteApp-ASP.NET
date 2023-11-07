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

		[HttpGet]
		public IActionResult AddNote()
        {
	        return View();
        }

		[HttpPost]
		public IActionResult AddNote(NoteViewModel note)
		{
			_noteViewModel.NoteViewModelList.Add(note);
			return RedirectToAction("Index");
		}

		public IActionResult EditNote()
        {

	        return View("Index");
        }

        public IActionResult DeleteNote()
        {
	        return View("Index");
		}

		[HttpPost]
        public IActionResult AcceptChanges()
        {
			return View("Index");
		}

        public IActionResult CancelChanges()
        {
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