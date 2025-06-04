using Microsoft.AspNetCore.Mvc;

namespace BooksawProject.WebUI.Controllers
{
    public class AdminController : Controller
    {
        public IActionResult AdminLayout()
        {
            return View();
        }

        public IActionResult AdminDashboard()
        {
            return View();
        }

    }
}
