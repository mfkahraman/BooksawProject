using AutoMapper;
using Booksaw.Business.Abstract;
using Booksaw.DataAccess.Abstract;
using Booksaw.Dto.BookDtos;
using Booksaw.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booksaw.Business.Concrete
{
    public class BookService(IBookDal bookDal, IMapper mapper) : IBookService
    {
        public void AddBook(CreateBookDto dto)
        {
            var book = mapper.Map<Book>(dto);
            bookDal.Add(book);
        }

        public void DeleteBook(int bookId)
        {
            bookDal.Delete(bookId);
        }

        public List<ResultBookDto> GetAllBooks()
        {
            var books = bookDal.GetAll();
            return mapper.Map<List<ResultBookDto>>(books);
        }

        public ResultBookDto GetBookById(int bookId)
        {
            var book = bookDal.GetById(bookId);
            return mapper.Map<ResultBookDto>(book);
        }

        public List<ResultBookDto> GetBooksByCategoryId(int categoryId)
        {
            var books = bookDal.GetBooksByCategoryId(categoryId);
            return mapper.Map<List<ResultBookDto>>(books);
        }

        public ResultBookDto GetRandomBook()
        {
            var book = bookDal.GetRandomBook();
            return mapper.Map<ResultBookDto>(book);
        }

        public void UpdateBook(UpdateBookDto dto)
        {
            var book = mapper.Map<Book>(dto);
            bookDal.Update(book);
        }


    }
}
