using GISA.Prestador.Context;
using GISA.Prestador.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
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

        public async Task<List<T>> Get()
        {
            return await _context.Set<T>().ToListAsync();
        }

        public async Task<T> GetById(int id)
        {
            return await _context.Set<T>().FindAsync(id);
        }

        public async Task<bool> Add(T entity)
        {
            await _context.Set<T>().AddAsync(entity);

            var result = await _context.SaveChangesAsync();

            return result > 0;
        }

        public async Task<bool> Update(T entity)
        {
            _context.Entry(entity).State = EntityState.Modified;

            var result = await _context.SaveChangesAsync();

            return result > 0;
        }

        public async Task<bool> Delete(T entity)
        {
            _context.Set<T>().Remove(entity);

            var result = await _context.SaveChangesAsync();

            return result > 0;
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
