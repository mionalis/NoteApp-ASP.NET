namespace NoteApp.Models
{
	public class Project
	{
		public Note CurrentNote { get; set; }

		public List<Note> Notes { get; set; }
	}
}
