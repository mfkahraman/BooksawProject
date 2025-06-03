using Microsoft.AspNetCore.Mvc;

namespace BooksawProject.WebUI.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult HomePage()
        {
            return View();
        }
    }
}
