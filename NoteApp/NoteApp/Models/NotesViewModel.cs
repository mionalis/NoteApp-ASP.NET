using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace NoteApp.Models
{
	/// <summary>
	/// Модель представления заметок.
	/// </summary>
	public class NotesViewModel
	{
		/// <summary>
		/// Создаёт экземпляр класса <see cref="NotesViewModel"/>.
		/// </summary>
		/// <param name="note">Заметка.</param>
		public NotesViewModel(Note note)
		{
			Note = note;
		}

		/// <summary>
		/// Создаёт экземпляр класса <see cref="NotesViewModel"/>.
		/// </summary>
		public NotesViewModel()
		{
		}

		/// <summary>
		/// Возвращает и задает заголовок заметки. Используется для привязки к NotesListBox.
		/// </summary>
		[BindProperty(Name = "NotesListBox")]
		public string Title
		{
			get => Note.Title;
			set => Note.Title = value;
		}

		/// <summary>
		/// Возвращает и задает текст заметки. Используется для привязки к NotesListBox.
		/// </summary>
		[BindProperty(Name = "NotesListBox")]
		public string Content
		{
			get => Note.Content;
			set => Note.Content = value;
		}

		/// <summary>
		/// Возвращает и задает категорию заметки. Используется для привязки к выпадающему списку.
		/// </summary>
		public NoteCategory Category
		{
			get => Note.Category;
			set => Note.Category = value;
		}

		/// <summary>
		/// Возвращает время создания заметки.
		/// </summary>
		public DateTime CreationTime => Note.CreationTime;

		/// <summary>
		/// Возвращает время последнего изменения заметки. Значение меняется при
		/// изменении названия, категории или текста заметки.
		/// </summary>
		public DateTime LastModifiedTime => Note.LastModifiedTime;

		/// <summary>
		/// Возвращает и задает заметку: экземпляр класса Note. 
		/// </summary>
		public Note Note { get; set; } = new();

		/// <summary>
		/// Возвращает и задает текущую заметку, выбранную в NotesListBox.
		/// </summary>
		public NotesViewModel CurrentNotes { get; set; }

		/// <summary>
		/// Возвращает и задает список заметок.
		/// </summary>
		public List<NotesViewModel> NotesList { get; set; } = new();

		/// <summary>
		///  Возвращает и задает список SelectListItem для привязки модели к NotesListBox.
		/// </summary>
		public List<SelectListItem> NotesSelectListItems { get; set; } = new();
	}
}
