using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace NoteApp.Models
{
	public class NoteViewModel
	{
		public NoteViewModel(Note note)
		{
			Note = note;
		}

		public NoteViewModel()
		{
		}

		[BindProperty(Name = "Aaa")]
		public string Title
		{
			get => Note.Title;
			set => Note.Title = value;
		}

		public string Content
		{
			get => Note.Content;
			set => Note.Content = value;
		}

		public Note Note { get; set; } = new Note();

		public NoteViewModel CurrentNote { get; set; }

		public List<SelectListItem> NoteSelectListItems { get; set; } = new();
	}
}
