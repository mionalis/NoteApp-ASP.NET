using Microsoft.AspNetCore.Mvc;
using NoteApp.Models;

namespace NoteApp.Services
{
    /// <summary>
    /// Выполняет валидацию значений.
    /// </summary>
    public static class Validator
    {
		/// <summary>
		/// Выполняет валидацию текстовых полей заметки.
		/// Длина заголовка не должна превышать 50 символов.
		/// </summary>
		/// <param name="note">Валидируемая заметка.</param>
		/// <returns>Кортеж с результатом валидации (True, если ошибка, False - если нет) и
		/// сообщением об ошибке.</returns>
		public static (bool HasError, string ErrorMessage) ValidateNote(Note note)
        {
            if (string.IsNullOrEmpty(note.Title) && note.Title.Length > 50)
            {
                return (true, "The Title length should not exceed 50 characters.");
            }

            return (false, "");
        }
    }
}
