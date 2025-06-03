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
    public class EfCategoryDal : GenericRepository<Category>, ICategoryDal
    {
        public EfCategoryDal(AppDbContext context) : base(context)
        {
        }
    }
}
