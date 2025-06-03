using Booksaw.Dto.BookDtos;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http;

namespace BooksawProject.WebUI.ViewComponents
{
    public class _FeaturedBooksComponent(IHttpClientFactory httpClient) : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var client = httpClient.CreateClient();
            var responseMessage = await client.GetAsync("https://localhost:7083/api/Book/GetAllBooks");
            var jsonData = await responseMessage.Content.ReadAsStringAsync();
            var values = JsonConvert.DeserializeObject<List<ResultBookDto>>(jsonData);
            var lastFourBooks = values.TakeLast(4).ToList();
            return View(lastFourBooks);
        }
    }
}
