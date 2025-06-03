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
    }
}
