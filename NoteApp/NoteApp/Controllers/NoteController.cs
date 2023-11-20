using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Rendering;
using NoteApp.Models;
using NoteApp.Services;

namespace NoteApp.Controllers
{
	/// <summary>
	/// Контроллер редактора заметок.
	/// </summary>
	public class NoteController : Controller
	{
		/// <summary>
		/// Выбрана ли категория в ComboBox для фильтрации заметок в NotesListBox.
		/// </summary>
		private static bool _isFiltering;

		/// <summary>
		/// Oбъект ILogger, необходимый для логгирования данных.
		/// </summary>
		private readonly ILogger<NoteController> _logger;

		/// <summary>
		/// Контекст данных.
		/// </summary>
		private NoteDbContext _noteDbContext;

		/// <summary>
		/// Модель представления заметок.
		/// </summary>
		private NotesViewModel _notesViewModel = new();

		/// <summary>
		/// Создаёт экземпляр класса <see cref="NoteController"/>.
		/// </summary>
		/// <param name="logger">Логгер.</param>
		/// <param name="noteDbContext">Контекст данных.</param>
		public NoteController(ILogger<NoteController> logger, NoteDbContext noteDbContext)
		{
			_logger = logger;
			_noteDbContext = noteDbContext;
		}

		/// <summary>
		/// Загружает главную страницу.
		/// </summary>
		/// <param name="id">ID отображаемой заметки.</param>
		/// <returns>Главная страница.</returns>
		[HttpGet]
		public IActionResult Index(int id)
		{
			if (id == 0)
			{
				GetNotesSelectListItems();
				return View(_notesViewModel);
			}

			var selectedNote = _noteDbContext.Notes.FirstOrDefault(note => note.ID == id);
			_notesViewModel.SelectedNote = selectedNote;

			if (_isFiltering)
			{
				GetFilteredSelectListItems(selectedNote.Category);
			}
			else
			{
				GetNotesSelectListItems();
			}

			return View(_notesViewModel);
		}

		/// <summary>
		/// Получает данные с главной страницы.
		/// </summary>
		/// <param name="id">ID выбранной заметки в NotesListBox.</param>
		/// <returns>Главная страница.</returns>
		[HttpPost]
		public IActionResult ShowNote(int id)
		{
			var selectedNote = _noteDbContext.Notes.FirstOrDefault(note => note.ID == id);
			_notesViewModel.SelectedNote = selectedNote;

			if (!_isFiltering)
			{
				GetNotesSelectListItems();
				return View("Index", _notesViewModel);
			}

			GetFilteredSelectListItems(selectedNote.Category);
			return View("Index", _notesViewModel);
		}

		/// <summary>
		/// При отмене действия перенапрявляет на главную страницу.
		/// </summary>
		/// <returns>Главная страница.</returns>
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
			var note = new Note();
			ViewBag.Message = "Add Note";
			_isFiltering = false;
			return View("AddEditNote", note);
		}

		/// <summary>
		/// Производит фильтрацию заметок в NotesListBox согласно выбранной категории. 
		/// </summary>
		/// <param name="id">ID выбранной категории заметки.</param>
		/// <returns>Главная страница.</returns>
		[HttpPost]
		public IActionResult FilterNotes(int id)
		{
			// Если в ComboBox выбрано "All".
			if (id == -1)
			{
				_isFiltering = false;
				GetNotesSelectListItems();

				return View("Index", _notesViewModel); 
			}

			_isFiltering = true;
			var noteCategory = (NoteCategory) id;
			GetFilteredSelectListItems(noteCategory);

			return View("Index", _notesViewModel);
		}

		/// <summary>
		/// Получает созданную заметку и добавляет ее в NotesListBox.
		/// </summary>
		/// <param name="note">Созданная заметка.</param>
		/// <returns>Главная страница.</returns>
		[HttpPost]
		public IActionResult AddNote(Note note)
		{
			if (Validator.ValidateNote(note).Item1)
			{
				ModelState.AddModelError("Title", (Validator.ValidateNote(note).Item2));
				ViewBag.Message = "Add Note";
				return View("AddEditNote", note);
			}

			_noteDbContext.Notes.Add(note);
			_noteDbContext.SaveChanges();

			return RedirectToAction("Index", new { id = note.ID });
		}

		/// <summary>
		/// Получает выбранную в NotesListBox заметку и отправляет ее на страницу редактирования
		/// или удаления.
		/// </summary>
		/// <param name="selectedListBoxObject">Выбранная заметка в NotesListBox.</param>
		/// <returns>Передача в методы <see cref="EditNote(int)"/> и <see cref="RemoveNote(int)"/>
		/// значения ID выбранной заметки.</returns>
		[HttpPost]
		public IActionResult GetValueFromListBox(
			NotesViewModel selectedListBoxObject, 
			string action)
		{
			if (selectedListBoxObject.ID == 0)
			{
				return RedirectToAction("Index");
			}

			return RedirectToAction(action, new { id = selectedListBoxObject.ID });
		}

		/// <summary>
		/// Загружает страницу редактирования выбранной заметки.
		/// </summary>
		/// <param name="id">ID выбранной заметки в NotesListBox.</param>
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
		/// <param name="note">Отредактированная заметка.</param>
		/// <returns>Главная страница.</returns>
		[HttpPost]
		public IActionResult EditNote(Note note)
		{
			if (Validator.ValidateNote(note).Item1)
			{
				ModelState.AddModelError("Title", (Validator.ValidateNote(note).Item2));
				ViewBag.Message = "Edit Note";
				return View("AddEditNote", note);
			}

			_noteDbContext.Notes.Update(note);
			_noteDbContext.Entry(note).Property(x => x.CreationTime).IsModified = false;
			_noteDbContext.SaveChanges();

			return RedirectToAction("Index",  new { id = note.ID });
		}

		/// <summary>
		/// Загружает страницу удаления заметки.
		/// </summary>
		/// <param name="id">ID выбранной заметка в NotesListBox.</param>
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
		/// <param name="note">Удаляемая заметка.</param>
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

		/// <summary>
		/// Добавляет объекты из списка заметок, отфильтрованного по одной категории, в список
		/// SelectListItem для их отображения в NotesListBox.
		/// </summary>
		/// <param name="category">Категория заметки.</param>
		private void GetFilteredSelectListItems(NoteCategory category)
		{
			var filteredNotes = _noteDbContext.Notes.Where(
				note => note.Category == category).ToList();

			foreach (var note in filteredNotes)
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