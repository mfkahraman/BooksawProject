using Booksaw.DataAccess.Abstract;
using Booksaw.DataAccess.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booksaw.DataAccess.Repositories
{
    public class GenericRepository<T> : IGenericDal<T> where T : class
    {
        private readonly AppDbContext _context;

        public GenericRepository(AppDbContext context)
        {
            _context = context;
        }

        public void Add(T entity)
        {
            _context.Set<T>().Add(entity);
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            var value = _context.Set<T>().Find(id);
            _context.Set<T>().Remove(value);
            _context.SaveChanges();
        }

        public List<T> GetAll()
        {
            var values = _context.Set<T>().ToList();
            return values;
        }

        public T GetById(int id)
        {
            var value = _context.Set<T>().Find(id);
            if (value == null)
            {
                throw new KeyNotFoundException($"Entity of type {typeof(T).Name} with ID {id} not found.");
            }
            return value;
        }

        public void Update(T entity)
        {
            _context.Set<T>().Update(entity);
            _context.SaveChanges();
        }
    }
}
