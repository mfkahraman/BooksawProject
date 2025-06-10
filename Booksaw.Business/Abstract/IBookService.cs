using Booksaw.Dto.BookDtos;
using Booksaw.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booksaw.Business.Abstract
{
    public interface IBookService
    {
        public void AddBook(CreateBookDto dto);
        public void UpdateBook(UpdateBookDto dto);
        public void DeleteBook(int bookId);
        public ResultBookDto GetBookById(int bookId);
        public List<ResultBookDto> GetAllBooks();
        public List<ResultBookDto> GetBooksByCategoryId(int categoryId);
        public ResultBookDto GetRandomBook();
    }
}
