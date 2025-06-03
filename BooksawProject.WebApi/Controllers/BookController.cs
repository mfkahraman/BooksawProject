using Booksaw.Business.Abstract;
using Booksaw.Dto.BookDtos;
using Booksaw.Entity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BooksawProject.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookController(IBookService bookService) : ControllerBase
    {
        [HttpGet("GetAllBooks")]
        public IActionResult GetAllBooks()
        {
            var values = bookService.GetAllBooks();
            if (values == null || !values.Any())
            {
                return NotFound("No books found.");
            }
            return Ok(values);
        }

        [HttpGet("GetBookById/{id}")]
        public IActionResult GetBookById(int id)
        {
            var value = bookService.GetBookById(id);
            if (value == null)
            {
                return NotFound();
            }
            return Ok(value);
        }

        [HttpGet("GetBooksByCategoryId/{id}")]
        public IActionResult GetBooksByCategoryId(int id)
        {
            var values = bookService.GetBooksByCategoryId(id);
            if (values == null || !values.Any())
            {
                return NotFound("No books found for this category.");
            }
            return Ok(values);
        }

        [HttpPost("AddBook")]
        public IActionResult AddBook(CreateBookDto dto)
        {

            bookService.AddBook(dto);
            return Ok("Kitap başarıyla oluşturuldu");
        }

        [HttpPut("UpdateBook")]
        public IActionResult UpdateBook(UpdateBookDto dto)
        {
            bookService.UpdateBook(dto);
            return Ok("Kitap başarıyla güncellendi");
        }

        [HttpDelete("DeleteBook/{id}")]
        public IActionResult DeleteBook(int id)
        {
            var book = bookService.GetBookById(id);
            if (book == null)
            {
                return NotFound("Kitap bulunamadı");
            }
            bookService.DeleteBook(id);
            return Ok("Kitap başarıyla silindi");
        }


    }
}
