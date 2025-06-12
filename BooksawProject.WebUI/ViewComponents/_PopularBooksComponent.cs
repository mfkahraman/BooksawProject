using Booksaw.Dto.BookDtos;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http;
using System.Threading.Tasks;
using System.Linq;

namespace BooksawProject.WebUI.ViewComponents
{
    public class _PopularBooksComponent : ViewComponent
    {
        private readonly IHttpClientFactory _httpClient;

        public _PopularBooksComponent(IHttpClientFactory httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var client = _httpClient.CreateClient();
            var responseMessage = await client.GetAsync("https://localhost:7083/api/Book/GetAllBooks");
            var jsonData = await responseMessage.Content.ReadAsStringAsync();
            var allBooks = JsonConvert.DeserializeObject<List<ResultBookDto>>(jsonData);

            // Group books by category and limit to 6 books per category
            var booksGroupedByCategory = allBooks
                .GroupBy(book => book.CategoryId)
                .Select(group => new
                {
                    CategoryId = group.Key,
                    CategoryName = group.First().Category.Name,  // Assuming category name is available in the book object
                    Books = group.Take(4).ToList()  // Take 6 books per category
                })
                .ToList();

            return View(booksGroupedByCategory);  // Pass grouped books to the view
        }
    }
}
