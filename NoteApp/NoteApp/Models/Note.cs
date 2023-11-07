namespace NoteApp.Models
{
	public class Note
	{
		public Note(string title, string content, NoteCategory category)
		{
			Title = title;
			Content = content;
			Category = category;
		}

		public Note()
		{
		}

		public string Title { get; set; } = "Без названия";

		public string Content { get; set; }

		public NoteCategory Category { get; set; }

		public DateTime CreationTime { get; private set; }

		public DateTime LastModifiedTime { get; private set; }
	}
}
