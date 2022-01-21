using System.Collections.Generic;
using System.Threading.Tasks;

namespace GISA.Associado.Repositories.Interfaces
{
    public interface IRepository<T>
    {
        Task<List<T>> Get();
        Task<T> GetById(int id);
        Task<bool> Add(T entity);
        Task<bool> Update(T entity);
        Task<bool> Delete(T entity);
    }
}
