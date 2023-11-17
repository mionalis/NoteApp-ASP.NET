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
		public NotesViewModel()
		{
		}

		/// <summary>
		/// Возвращает и задает ID заметки. Используется для получения выбранного элемента из
		/// NotesListBox.
		/// </summary>
		[BindProperty(Name = "NotesListBox")]
		public int ID { get; set; }

		[BindProperty(Name = "NoteCategoryComboBox")]
		public NoteCategory NoteCategory { get; set; }

		/// <summary>
		/// Возвращает и задает текущую заметку, выбранную в NotesListBox.
		/// </summary>
		[BindProperty(Name = "NotesListBox")]
		public Note SelectedNote { get; set; }

		/// <summary>
		///  Возвращает и задает список SelectListItem для привязки модели к NotesListBox.
		/// </summary>
		public List<SelectListItem> NotesSelectListItems { get; set; } = new();
	}
}
