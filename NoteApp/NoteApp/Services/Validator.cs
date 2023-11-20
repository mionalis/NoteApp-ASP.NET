using Microsoft.AspNetCore.Mvc;
using NoteApp.Models;

namespace NoteApp.Services
{
    public static class Validator
    {
		/// <summary>
		/// Выполняет валидацию текстовых полей заметки.
		/// Длина заголовка не должна превышать 50 символов.
		/// </summary>
		/// <param name="note">Валидируемая заметка.</param>
		/// <returns>Кортеж с результатом валидации (True, если ошибка, False - если нет) и
		/// сообщением об ошибке.</returns>
		public static (bool, string) ValidateNote(Note note)
        {
            if (note.Title != null && note.Title.Length > 50)
            {
                return (true, "The Title length should not exceed 50 characters.");
            }

            return (false, "");
        }
    }
}
