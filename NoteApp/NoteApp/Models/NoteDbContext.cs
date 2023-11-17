using Microsoft.EntityFrameworkCore;

namespace NoteApp.Models
{
	/// <summary>
	/// Реализует связь с базой данных.
	/// </summary>
	public class NoteDbContext : DbContext
	{
		/// <summary>
		/// Создаёт экземпляр класса <see cref="NoteDbContext"/>.
		/// </summary>
		/// <param name="options">Настройки контекста данных.</param>
		public NoteDbContext(DbContextOptions<NoteDbContext> options) : base(options)
		{
			Database.EnsureCreated();
		}

		/// <summary>
		/// Возвращает и задает коллекцию объектов, которая сопоставляется с таблицей Note в базе данных.
		/// </summary>
		public DbSet<Note> Notes { get; set; }
	}
}
