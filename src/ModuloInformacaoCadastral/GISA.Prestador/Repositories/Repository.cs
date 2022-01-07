using GISA.Prestador.Context;
using GISA.Prestador.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GISA.Prestador.Repositories
{
    public class Repository<T> : IRepository<T> where T : class
    {
        protected PrestadorContext _context;
        public Repository(PrestadorContext context)
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

        public void Add(T entity)
        {
            _context.Set<T>().Add(entity);
            _context.SaveChanges();
        }

        public void Update(T entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
            _context.SaveChanges();
        }

        public void Delete(T entity)
        {
            _context.Set<T>().Remove(entity);
            _context.SaveChanges();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
