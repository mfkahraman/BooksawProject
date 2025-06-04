using Booksaw.Dto.CategoryDtos;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http;

namespace BooksawProject.WebUI.Controllers
{
    public class AdminCategoryController(IHttpClientFactory httpClient) : Controller
    {
        [HttpGet]
        public async Task<IActionResult> CategoryList()
        {
            var client = httpClient.CreateClient();
            var responseMessage = await client
                .GetAsync("https://localhost:7083/api/Book/GetAllCategories");
            if (responseMessage.IsSuccessStatusCode)
            {
                var jsonData = await responseMessage.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<List<ResultCategoryDto>>(jsonData);
                return View(values);
            }
            return View();
        }
    }
}
