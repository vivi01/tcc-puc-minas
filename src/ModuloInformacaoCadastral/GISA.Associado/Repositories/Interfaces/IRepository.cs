using System.Linq;

namespace GISA.Associado.Repositories.Interfaces
{
    public interface IRepository<T>
    {
        IQueryable<T> Get();
        T GetById(int id);
        void Add(T entity);
        void Update(T entity);
        void Delete(T entity);
    }
}
