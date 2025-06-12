using Microsoft.AspNetCore.Mvc;

namespace BooksawProject.WebUI.ViewComponents
{
    public class _SubscribeComponent : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
