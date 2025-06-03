using Booksaw.Dto.BookDtos;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Threading.Tasks;

namespace BooksawProject.WebUI.ViewComponents
{
    public class _BillboardComponent(IHttpClientFactory httpClient) : ViewComponent
    {

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var client = httpClient.CreateClient();
            var responseMessage = await client.GetAsync("https://localhost:7083/api/Book/GetAllBooks");
            var jsonData = await responseMessage.Content.ReadAsStringAsync();
            var values = JsonConvert.DeserializeObject<List<ResultBookDto>>(jsonData);
            return View(values);
        }
    }
}
