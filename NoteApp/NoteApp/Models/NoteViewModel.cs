using Microsoft.AspNetCore.Mvc.Rendering;

namespace NoteApp.Models
{
	public class NoteViewModel
	{
		public Note CurrentNote { get; set; }

		public IEnumerable<int>? ListBoxNotesID { get; set; }

		public IEnumerable<SelectListItem>? ListBoxNotes { get; set; }
	}
}
