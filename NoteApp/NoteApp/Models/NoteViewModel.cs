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

		[BindProperty(Name = "NotesListBox")]
		public string Title
		{
			get => Note.Title;
			set => Note.Title = value;
		}

		[BindProperty(Name = "NotesListBox")]
		public string Content
		{
			get => Note.Content;
			set => Note.Content = value;
		}

		public Note Note { get; set; } = new();

		public NoteViewModel CurrentNote { get; set; }

		public List<NoteViewModel> NoteViewModelList = new();

		public List<SelectListItem> NotesSelectListItems { get; set; } = new();
	}
}
