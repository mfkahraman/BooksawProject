﻿using Booksaw.Dto.BookDtos;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http;

namespace BooksawProject.WebUI.ViewComponents
{
    public class _BookOfTheDayComponent (IHttpClientFactory httpClient) : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var client = httpClient.CreateClient();
            var responseMessage = await client.GetAsync("https://localhost:7083/api/Book/GetRandomBook");
            var jsonData = await responseMessage.Content.ReadAsStringAsync();
            var book = JsonConvert.DeserializeObject<ResultBookDto>(jsonData);
            return View(book);
        }
    }
}
