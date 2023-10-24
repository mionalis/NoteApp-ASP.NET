using Microsoft.AspNetCore.Mvc;

namespace NoteApp.Controllers
{
    public class AboutController : Controller
    {
        public IActionResult About()
        {
            return View();
        }
    }
}
