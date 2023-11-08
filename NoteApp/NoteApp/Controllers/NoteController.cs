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
	/// <summary>
	/// Контроллер редактора заметок.
	/// </summary>
	public class NoteController : Controller
	{
		private readonly ILogger<NoteController> _logger;

		public NoteController(ILogger<NoteController> logger)
		{
			_logger = logger;
		}

		/// <summary>
		/// Возвращает и задает модель представления заметок.
		/// </summary>
		private NoteViewModel NoteViewModel { get; set; } = new();

		/// <summary>
		/// Загружает главную страницу.
		/// </summary>
		/// <returns>Главная страница.</returns>
		[HttpGet]
		public IActionResult Index()
		{
			InitializeNoteListForTesting();
			NoteViewModel.NotesSelectListItems = GetNotesSelectListItems();

			return View(NoteViewModel);
		}

		/// <summary>
		/// Получает данные с главной страницы.
		/// </summary>
		/// <param name="noteViewModel">Выбранная заметка в NotesListBox.</param>
		/// <returns>Главная страница.</returns>
		[HttpPost]
		public IActionResult Index(NoteViewModel noteViewModel)
		{
			InitializeNoteListForTesting();

			var selectedNote = NoteViewModel.NotesList.FirstOrDefault(
				c => c.Title == noteViewModel.Title);

			NoteViewModel.CurrentNote = selectedNote;

			NoteViewModel.NotesSelectListItems = GetNotesSelectListItems();
			return View(NoteViewModel);
		}

		/// <summary>
		/// Загружает страницу добавления новой заметки.
		/// </summary>
		/// <returns>Страница добавления новой заметки.</returns>
		[HttpGet]
		public IActionResult AddNote()
        {
	        return View();
        }

		/// <summary>
		/// Получает созданную заметку и добавляет ее в NotesListBox.
		/// </summary>
		/// <param name="noteViewModel">Созданная заметка.</param>
		/// <returns>Главная страница.</returns>
		[HttpPost]
		public IActionResult AddNote(NoteViewModel noteViewModel)
		{
			return RedirectToAction("Index");
		}

		/// <summary>
		/// Загружает страницу редактирования выбранной заметки.
		/// </summary>
		/// <param name="noteViewModel">Выбранная заметка в NotesListBox.</param>
		/// <returns>Страница редактирования выбранной заметки.</returns>
		[HttpGet]
		public IActionResult EditNote(NoteViewModel noteViewModel)
		{
			/*	var selectedNote = NoteViewModel.NotesList.FirstOrDefault(
					c => c.Title == noteViewModel.Title);*/

			var selectedNote = new NoteViewModel();

			if (selectedNote == null)
			{
				return RedirectToAction("Index");
			}

			return View(selectedNote);
		}

		/// <summary>
		/// Получает отредактированную заметку и обновляет ее в NotesListBox.
		/// </summary>
		/// <param name="noteViewModel">Отредактированная заметка.</param>
		/// <returns>Главная страница.</returns>
		[HttpPost]
		public IActionResult AcceptModifiedNote(NoteViewModel noteViewModel)
		{
			return RedirectToAction("Index");
		}

		/// <summary>
		/// Загружает страницу удаления заметки.
		/// </summary>
		/// <param name="noteViewModel">Выбранная заметка в NotesListBox.</param>
		/// <returns>Страница удаления заметки.</returns>
		[HttpGet]
		public IActionResult DeleteNote(NoteViewModel noteViewModel)
		{
			/*	var selectedNote = NoteViewModel.NotesList.FirstOrDefault(
					c => c.Title == noteViewModel.Title);*/

			var selectedNote = new NoteViewModel();

			if (selectedNote == null)
			{
				return RedirectToAction("Index");
			}

			return View(selectedNote);
		}

		/// <summary>
		/// Выполняет удаление заметки из NotesListBox.
		/// </summary>
		/// <param name="noteViewModel">Удаляемая заметка.</param>
		/// <returns>Главная страница.</returns>
		[HttpPost]
		public IActionResult AcceptNoteDeletion(NoteViewModel noteViewModel)
		{
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
		private List<SelectListItem> GetNotesSelectListItems()
        {
	        var notesSelectListItems = new List<SelectListItem>();

	        foreach (var note in NoteViewModel.NotesList)
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

		/// <summary>
		/// Инициализирует список заметок значениями для отладки. Временный метод.
		/// </summary>
        private void InitializeNoteListForTesting()
        {
	        NoteViewModel.NotesList = new List<NoteViewModel>()
	        {
		        new() { Title = "Note1", Content = "Content" },
		        new() { Title = "Note2", Content = "Content" },
		        new() { Title = "Note3", Content = "Content" }
			};
        }
	}
}