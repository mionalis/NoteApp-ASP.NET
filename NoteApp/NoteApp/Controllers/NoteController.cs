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
	public class NoteController : Controller
	{
		private readonly ILogger<NoteController> _logger;

		public NoteController(ILogger<NoteController> logger)
		{
			_logger = logger;
		}

		private NoteViewModel _noteViewModel { get; set; } = new();

		[HttpGet]
		public IActionResult Index()
		{
			InitializeNoteListForTesting();
			_noteViewModel.NotesSelectListItems = GetNotesSelectListItems();

			return View(_noteViewModel);
		}

		[HttpPost]
		public IActionResult Index(NoteViewModel noteViewModel)
		{
			InitializeNoteListForTesting();

			var selectedNote = _noteViewModel.NoteViewModelList.FirstOrDefault(
				c => c.Title == noteViewModel.Title);

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
		public IActionResult AddNote(NoteViewModel noteViewModel)
		{
			return RedirectToAction("Index");
		}

		[HttpGet]
		public IActionResult EditNote(NoteViewModel noteViewModel)
		{
			/*	var selectedNote = _noteViewModel.NoteViewModelList.FirstOrDefault(
					c => c.Title == noteViewModel.Title);*/

			var selectedNote = new NoteViewModel();

			if (selectedNote == null)
			{
				return RedirectToAction("Index");
			}

			return View(selectedNote);
		}

		[HttpPost]
		public IActionResult AcceptModifiedNote(NoteViewModel noteViewModel)
		{
			return RedirectToAction("Index");
		}

		[HttpGet]
		public IActionResult DeleteNote(NoteViewModel noteViewModel)
		{
			/*	var selectedNote = _noteViewModel.NoteViewModelList.FirstOrDefault(
					c => c.Title == noteViewModel.Title);*/

			var selectedNote = new NoteViewModel();

			if (selectedNote == null)
			{
				return RedirectToAction("Index");
			}

			return View(selectedNote);
		}

		[HttpPost]
		public IActionResult AcceptNoteDeletion(NoteViewModel noteViewModel)
		{
			return RedirectToAction("Index");
		}

		private List<SelectListItem> GetNotesSelectListItems()
        {
	        var notesSelectListItems = new List<SelectListItem>();

	        foreach (var note in _noteViewModel.NoteViewModelList)
	        {
		        var selectList = new SelectListItem()
		        {
			        Text = note.Title,
			        Value = note.Title,
					Selected = false
		        };

		        notesSelectListItems.Add(selectList);
	        }

			return notesSelectListItems;
        }

        private void InitializeNoteListForTesting()
        {
	        _noteViewModel.NoteViewModelList = new List<NoteViewModel>()
	        {
		        new() { Title = "Note1", Content = "Content" },
		        new() { Title = "Note2", Content = "Content" },
		        new() { Title = "Note3", Content = "Content" }
			};
        }
	}
}