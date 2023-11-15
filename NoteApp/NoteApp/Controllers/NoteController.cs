using Microsoft.AspNetCore.Mvc;
using NoteApp.Models;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Security.Principal;
using System.Text;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace NoteApp.Controllers
{
	/// <summary>
	/// Контроллер редактора заметок.
	/// </summary>
	public class NoteController : Controller
	{
		/// <summary>
		/// Oбъект ILogger, необходимый для логгирования данных.
		/// </summary>
		private readonly ILogger<NoteController> _logger;

		private NoteDbContext _noteDbContext;

		/// <summary>
		/// Модель представления заметок.
		/// </summary>
		private NotesViewModel _notesViewModel = new();

		/// <summary>
		/// Создаёт экземпляр класса <see cref="NoteController"/>.
		/// </summary>
		/// <param name="logger">Логгер.</param>
		public NoteController(ILogger<NoteController> logger, NoteDbContext noteDbContext)
		{
			_logger = logger;
			_noteDbContext = noteDbContext;
		}

		/// <summary>
		/// Загружает главную страницу.
		/// </summary>
		/// <returns>Главная страница.</returns>
		[HttpGet]
		public IActionResult Index()
		{
			GetNotesSelectListItems();
			return View(_notesViewModel);
		}

		/// <summary>
		/// Получает данные с главной страницы.
		/// </summary>
		/// <param name="notesViewModel">Выбранная заметка в NotesListBox.</param>
		/// <returns>Главная страница.</returns>
		[HttpPost]
		public IActionResult Index(NotesViewModel selectedListBoxObject)
		{
			var selectedNote = _noteDbContext.Notes.FirstOrDefault(
				note => note.ID == selectedListBoxObject.ID);

			_notesViewModel.SelectedNote = selectedNote;

			GetNotesSelectListItems();
			return View(_notesViewModel);
		}

		public IActionResult CancelAction()
		{
			return RedirectToAction("Index");
		}

		/// <summary>
		/// Загружает страницу добавления новой заметки.
		/// </summary>
		/// <returns>Страница добавления новой заметки.</returns>
		[HttpGet]
		public IActionResult AddNote()
        {
			ViewBag.Message = "Add Note";
			return View("AddEditNote");
		}

		/// <summary>
		/// Получает созданную заметку и добавляет ее в NotesListBox.
		/// </summary>
		/// <param name="notesViewModel">Созданная заметка.</param>
		/// <returns>Главная страница.</returns>
		[HttpPost]
		public IActionResult AddNote(Note note)
		{
			_noteDbContext.Notes.Add(note); 
			_noteDbContext.SaveChanges();

			return RedirectToAction("Index");
		}

		[HttpPost]
		public IActionResult GetValueForEditing(NotesViewModel selectedListBoxObject)
		{
			var selectedNoteID = selectedListBoxObject.ID;

			if (selectedNoteID == 0)
			{
				return RedirectToAction("Index");
			}

			GetNotesSelectListItems();
			return RedirectToAction("EditNote", new { id = selectedNoteID });
		}

		/// <summary>
		/// Загружает страницу редактирования выбранной заметки.
		/// </summary>
		/// <param name="notesViewModel">Выбранная заметка в NotesListBox.</param>
		/// <returns>Страница редактирования выбранной заметки.</returns>
		[HttpGet]
		public IActionResult EditNote(int id)
		{
			var selectedNote = _noteDbContext.Notes.FirstOrDefault(note => note.ID == id);

			if (selectedNote == null)
			{
				return RedirectToAction("Index");
			}

			ViewBag.Message = "Edit Note";
			return View("AddEditNote", selectedNote);
		}

		/// <summary>
		/// Получает отредактированную заметку и обновляет ее в NotesListBox.
		/// </summary>
		/// <param name="notesViewModel">Отредактированная заметка.</param>
		/// <returns>Главная страница.</returns>
		[HttpPost]
		public IActionResult EditNote(Note note)
		{
			_noteDbContext.Notes.Update(note);
			_noteDbContext.SaveChanges();

			return RedirectToAction("Index");
		}

		[HttpPost]
		public IActionResult GetValueForRemoving(NotesViewModel selectedListBoxObject)
		{
			var selectedNoteID = selectedListBoxObject.ID;

			if (selectedNoteID == 0)
			{
				return RedirectToAction("Index");
			}

			GetNotesSelectListItems();
			return RedirectToAction("RemoveNote", new { id = selectedNoteID });
		}

		/// <summary>
		/// Загружает страницу удаления заметки.
		/// </summary>
		/// <param name="notesViewModel">Выбранная заметка в NotesListBox.</param>
		/// <returns>Страница удаления заметки.</returns>
		[HttpGet]
		public IActionResult RemoveNote(int id)
		{
			var selectedNote = _noteDbContext.Notes.FirstOrDefault(note => note.ID == id);

			if (selectedNote == null)
			{
				return RedirectToAction("Index");
			}

			return View(selectedNote);
		}

		/// <summary>
		/// Выполняет удаление заметки из NotesListBox.
		/// </summary>
		/// <param name="notesViewModel">Удаляемая заметка.</param>
		/// <returns>Главная страница.</returns>
		[HttpPost]
		public IActionResult RemoveNote(Note note)
		{
			_noteDbContext.Notes.Remove(note);
			_noteDbContext.SaveChanges();

			return RedirectToAction("Index");
		}

		/// <summary>
		/// Загружает страницу информации о сайте.
		/// </summary>
		/// <returns></returns>
		public IActionResult About()
		{
			return View();
		}

		/// <summary>
		/// Добавляет объекты из списка заметок в список SelectListItem для их отображения
		/// в NotesListBox.
		/// </summary>
		/// <returns>Список SelectListItem.</returns>
		private void GetNotesSelectListItems()
        {
	        foreach (var note in _noteDbContext.Notes)
	        {
		        var selectList = new SelectListItem()
		        {
			        Text = note.Title,
			        Value = note.ID.ToString()
		        };

		        _notesViewModel.NotesSelectListItems.Add(selectList);
	        }
        }
	}
}