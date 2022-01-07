using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GISA.Prestador.Repositories.Interfaces
{
    public interface IRepository <T>
    {
        IQueryable<T> Get();
        T GetById(int id);
        void Add(T entity);
        void Update(T entity);
        void Delete(T entity);
    }
}
