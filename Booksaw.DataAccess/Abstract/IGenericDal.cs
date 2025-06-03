using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booksaw.DataAccess.Abstract
{
    public interface IGenericDal<T> where T : class
    {
        public void Add(T entity);
        public void Update(T entity);
        public void Delete(int id);
        public T GetById(int id);
        public List<T> GetAll();
    }
}
