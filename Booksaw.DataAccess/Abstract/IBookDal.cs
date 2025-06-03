using Booksaw.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booksaw.DataAccess.Abstract
{
    public interface IBookDal : IGenericDal<Book>
    {
        public List<Book> GetBooksByCategoryId(int categoryId);
    }
}
