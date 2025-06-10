using Booksaw.DataAccess.Abstract;
using Booksaw.DataAccess.Context;
using Booksaw.DataAccess.Repositories;
using Booksaw.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booksaw.DataAccess.Concrete
{
    public class EfBookDal(AppDbContext context) : GenericRepository<Book>(context), IBookDal
    {
        public List<Book> GetBooksByCategoryId(int categoryId)
        {
            var values = context.Books
                .Where(b => b.CategoryId == categoryId)
                .ToList();
            if (values == null || !values.Any())
            {
                throw new KeyNotFoundException($"No books found for category ID {categoryId}.");
            }
            return values;
        }

        public Book GetRandomBook()
        {
            int booksCount = context.Books.Count();
            if (booksCount == 0)
            {
                throw new InvalidOperationException("No books available to select a random book.");
            }
            var randomIndex = new Random().Next(0, booksCount);
            var randomBook = context.Books.Skip(randomIndex).FirstOrDefault();
            if (randomBook == null)
            {
                throw new InvalidOperationException("Failed to retrieve a random book.");
            }
            return randomBook;
        }
    }
}
