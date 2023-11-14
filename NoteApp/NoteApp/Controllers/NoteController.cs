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
		/// <summary>
		/// Oбъект ILogger, необходимый для логгирования данных.
		/// </summary>
		private readonly ILogger<NoteController> _logger;

		/// <summary>
		/// Создаёт экземпляр класса <see cref="NoteController"/>.
		/// </summary>
		/// <param name="logger">Логгер.</param>
		public NoteController(ILogger<NoteController> logger)
		{
			_logger = logger;
		}

		/// <summary>
		/// Модель представления заметок.
		/// </summary>
		private NotesViewModel _notesViewModel = new();

		/// <summary>
		/// Загружает главную страницу.
		/// </summary>
		/// <returns>Главная страница.</returns>
		[HttpGet]
		public IActionResult Index()
		{
			InitializeNoteListForTesting();
			_notesViewModel.NotesSelectListItems = GetNotesSelectListItems();

			return View(_notesViewModel);
		}

		/// <summary>
		/// Получает данные с главной страницы.
		/// </summary>
		/// <param name="notesViewModel">Выбранная заметка в NotesListBox.</param>
		/// <returns>Главная страница.</returns>
		[HttpPost]
		public IActionResult Index(NotesViewModel notesViewModel)
		{
			InitializeNoteListForTesting();

			var selectedNote = _notesViewModel.NotesList.FirstOrDefault(
				note => note.ID == notesViewModel.ID);

			_notesViewModel.SelectedNote = selectedNote;

			_notesViewModel.NotesSelectListItems = GetNotesSelectListItems();
			return View(_notesViewModel);
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
		public IActionResult AddNote(Note selectedNote)
		{
			return RedirectToAction("Index");
		}

		/// <summary>
		/// Загружает страницу редактирования выбранной заметки.
		/// </summary>
		/// <param name="notesViewModel">Выбранная заметка в NotesListBox.</param>
		/// <returns>Страница редактирования выбранной заметки.</returns>
		[HttpGet]
		public IActionResult EditNote(NotesViewModel selectedListBoxObject)
		{
			// Получение выбранной заметки из ListBox. Закомментировано, чтобы продемонстрировать
			// страницу редактирования, потому что на данный момент функция не работает
			// корректно.
			/*	var selectedNote = _notesViewModel.NotesList.FirstOrDefault(
					c => c.Title == selectedListBoxObject.Title);*/

			// Добавлено для демонстрации страницы редактирования заметки.
			var selectedNote = new Note();

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
			return RedirectToAction("Index");
		}

		/// <summary>
		/// Загружает страницу удаления заметки.
		/// </summary>
		/// <param name="notesViewModel">Выбранная заметка в NotesListBox.</param>
		/// <returns>Страница удаления заметки.</returns>
		[HttpGet]
		public IActionResult RemoveNote(Note selectedListBoxObject)
		{
			// Получение выбранной заметки из ListBox. Закомментировано, чтобы продемонстрировать
			// страницу удаления, потому что на данный момент функция не работает корректно.
			/*	var selectedNote = _notesViewModel.NotesList.FirstOrDefault(
					c => c.Title == selectedListBoxObject.Title);*/

			// Добавлено для демонстрации страницы удаления заметки.
			var selectedNote = new Note();

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
		public IActionResult AcceptNoteDeletion(Note note)
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

	        foreach (var note in _notesViewModel.NotesList)
	        {
		        var selectList = new SelectListItem()
		        {
			        Text = note.Title,
			        Value = note.ID.ToString(),
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
	        _notesViewModel.NotesList = new List<Note>()
	        {
		        new() {  ID = 1, Title = "Note1", Content = "Content" },
		        new() {  ID = 2, Title = "Note2", Content = "Content" },
		        new() {  ID = 3, Title = "Note3", Content = "Content" }
			};
        }
	}
}