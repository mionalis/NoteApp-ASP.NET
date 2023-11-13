using Microsoft.EntityFrameworkCore;

namespace NoteApp.Models
{
	public class NoteDbContext : DbContext
	{
		public DbSet<Note> Notes { get; set; }

		public NoteDbContext(DbContextOptions<NoteDbContext> options) : base(options)
		{
			Database.EnsureCreated();
		}
	}
}
