using Microsoft.AspNetCore.Mvc;

namespace BooksawProject.WebUI.ViewComponents
{
    public class _LayoutHeadComponent : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
