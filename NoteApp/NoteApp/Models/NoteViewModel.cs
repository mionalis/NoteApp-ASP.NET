using Microsoft.AspNetCore.Mvc.Rendering;

namespace NoteApp.Models
{
	public class NoteViewModel
	{
		public Note CurrentNote { get; set; }

		public IEnumerable<int>? SelectedNotes { get; set; }

		public IEnumerable<SelectListItem>? Notes { get; set; }
	}
}
