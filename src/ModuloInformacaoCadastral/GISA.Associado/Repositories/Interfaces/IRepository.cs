using System.Linq;

namespace GISA.Associado.Repositories.Interfaces
{
    public interface IRepository<T>
    {
        IQueryable<T> Get();
        T GetById(int id);
        bool Add(T entity);
        bool Update(T entity);
        bool Delete(T entity);
    }
}
