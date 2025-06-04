using Booksaw.Business.Abstract;
using Booksaw.Dto.BookDtos;
using Booksaw.Dto.CategoryDtos;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Caching.Memory;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc.Rendering;
using X.PagedList.Extensions;

namespace BooksawProject.WebUI.Controllers
{
    public class AdminBookController : Controller
    {
        private readonly HttpClient _client;
        private readonly IImageService _imageService;
        private readonly IMemoryCache _memoryCache;  // Cache service

        public AdminBookController(IHttpClientFactory httpClient,
                                   IImageService imageService,
                                   IMemoryCache memoryCache)
        {
            _client = httpClient.CreateClient();
            _imageService = imageService;
            _memoryCache = memoryCache;
        }

        // Method to fetch categories (with optional caching)
        private async Task<List<ResultCategoryDto>> GetCategories()
        {
            // Try to get categories from memory cache first
            if (!_memoryCache.TryGetValue("categories", out List<ResultCategoryDto> categories))
            {
                var responseMessage = await _client.GetAsync("https://localhost:7083/api/Category/GetAllCategories");
                if (responseMessage.IsSuccessStatusCode)
                {
                    var jsonData = await responseMessage.Content.ReadAsStringAsync();
                    categories = JsonConvert.DeserializeObject<List<ResultCategoryDto>>(jsonData);

                    // Cache the categories with sliding expiration of 30 minutes
                    var cacheEntryOptions = new MemoryCacheEntryOptions()
                        .SetSlidingExpiration(TimeSpan.FromMinutes(30));  // Cache expiration time
                    _memoryCache.Set("categories", categories, cacheEntryOptions);
                }
            }

            return categories ?? new List<ResultCategoryDto>();  // Return an empty list if categories could not be loaded
        }

        public async Task<IActionResult> Index(int page = 1, int pageSize = 8)
        {
            var responseMessage = await _client.GetAsync("https://localhost:7083/api/Book/GetAllBooks");
            var jsonData = await responseMessage.Content.ReadAsStringAsync();
            var values = JsonConvert.DeserializeObject<List<ResultBookDto>>(jsonData).ToPagedList(page, pageSize);
            return View(values);
        }

        [HttpGet]
        public async Task<IActionResult> CreateBook()
        {
            var categories = await GetCategories();
            ViewBag.categories = (from x in categories
                                  select new SelectListItem
                                  {
                                      Text = x.Name,
                                      Value = x.CategoryId.ToString()
                                  }).ToList();
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateBook(CreateBookDto model)
        {
            if (model.ImageFile != null)
            {
                // Save the image and get the image path (URL)
                var imagePath = await _imageService.SaveImageAsync(model.ImageFile, "books");
                model.ImageUrl = imagePath;
            }

            // Prepare the categories for the dropdown list
            var categories = await GetCategories();
            ViewBag.categories = categories.Select(x => new SelectListItem
            {
                Text = x.Name,
                Value = x.CategoryId.ToString()
            }).ToList();

            // Create MultipartFormDataContent to send both the model and the file
            var content = new MultipartFormDataContent();

            // Add the model properties as form data
            content.Add(new StringContent(model.Title), "Title");
            content.Add(new StringContent(model.Author), "Author");
            content.Add(new StringContent(model.Description), "Description");
            content.Add(new StringContent(model.Price.ToString()), "Price");
            content.Add(new StringContent(model.CategoryId.ToString()), "CategoryId");
            content.Add(new StringContent(model.ImageUrl), "ImageUrl");

            // Add the file as form data
            if (model.ImageFile != null)
            {
                var fileContent = new StreamContent(model.ImageFile.OpenReadStream());
                fileContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue(model.ImageFile.ContentType);
                content.Add(fileContent, "ImageFile", model.ImageFile.FileName);
            }

            // Send the request
            var response = await _client.PostAsync("https://localhost:7083/api/Book/AddBook", content);

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }

            // Handle failure (return the model with an error message)
            ModelState.AddModelError("", "An error occurred while creating the book.");
            return View(model);
        }



        [HttpGet]
        public async Task<IActionResult> UpdateBook(int id)
        {
            // Fetch the current book data from the API
            var responseMessage = await _client.GetAsync($"https://localhost:7083/api/Book/GetBookById/{id}");
            if (responseMessage.IsSuccessStatusCode)
            {
                var jsonData = await responseMessage.Content.ReadAsStringAsync();
                var book = JsonConvert.DeserializeObject<UpdateBookDto>(jsonData);

                // Fetch categories for the dropdown
                var categories = await GetCategories();
                ViewBag.categories = categories.Select(x => new SelectListItem
                {
                    Text = x.Name,
                    Value = x.CategoryId.ToString()
                }).ToList();

                return View(book);  // Pass the book to the view to pre-fill the form
            }

            return RedirectToAction("Index");  // Redirect if the book doesn't exist or API call fails
        }

        [HttpPost]
        public async Task<IActionResult> UpdateBook(UpdateBookDto dto)
        {
            if (dto.ImageFile != null)
            {
                // Save the image and get the image path (URL)
                var imagePath = await _imageService.SaveImageAsync(dto.ImageFile, "books");
                dto.ImageUrl = imagePath;
            }

            // Prepare the categories for the dropdown list
            var categories = await GetCategories();
            ViewBag.categories = categories.Select(x => new SelectListItem
            {
                Text = x.Name,
                Value = x.CategoryId.ToString()
            }).ToList();

            // Prepare MultipartFormDataContent to send both the model and the file
            var content = new MultipartFormDataContent();

            // Add the model properties as form data (including BookId)
            content.Add(new StringContent(dto.BookId.ToString()), "BookId");  // Include BookId
            content.Add(new StringContent(dto.Title), "Title");
            content.Add(new StringContent(dto.Author), "Author");
            content.Add(new StringContent(dto.Description), "Description");
            content.Add(new StringContent(dto.Price.ToString()), "Price");
            content.Add(new StringContent(dto.CategoryId.ToString()), "CategoryId");
            content.Add(new StringContent(dto.ImageUrl), "ImageUrl");  // Assuming ImageUrl is being updated

            // Add the file as form data (only if a file is uploaded)
            if (dto.ImageFile != null)
            {
                var fileContent = new StreamContent(dto.ImageFile.OpenReadStream());
                fileContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue(dto.ImageFile.ContentType);
                content.Add(fileContent, "ImageFile", dto.ImageFile.FileName);  // "ImageFile" is the name of the form field
            }

            // Send the POST request with the multipart form data
            var responseMessage = await _client.PutAsync("https://localhost:7083/api/Book/UpdateBook", content);

            if (responseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }

            // Handle failure and return the model with errors
            ModelState.AddModelError("", "Kitap güncellenirken bir hata oluştu.");
            return View(dto);
        }




        public async Task<IActionResult> DeleteBook(int id)
        {
            var responseMessage = await _client.DeleteAsync($"https://localhost:7083/api/Book/DeleteBook/{id}");
            if (responseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }

            return View();
        }
    }
}
