using GISA.Associado.Context;
using GISA.Associado.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace GISA.Associado.Repositories
{
    public class Repository<T> : IRepository<T> where T : class
    {
        protected AssociadoContext _context;
        public Repository(AssociadoContext context)
        {
            _context = context;
        }

        public IQueryable<T> Get()
        {
            return _context.Set<T>();
        }

        public T GetById(int id)
        {
            return _context.Set<T>().Find(id);
        }

        public bool Add(T entity)
        {
            _context.Set<T>().Add(entity);
            var result = _context.SaveChanges();

            return result > 0;
        }

        public bool Update(T entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
            _context.SaveChanges();

            var result = _context.SaveChanges();

            return result > 0;
        }

        public bool Delete(T entity)
        {
            _context.Set<T>().Remove(entity);
            var result = _context.SaveChanges();
            return result > 0;
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
