using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetworkOfShops.Repositories.Interfaces
{
    public interface IGenericRepository<T> where T : class
    {
        Task<IEnumerable<T>> Get();
        Task<T> GetByID(object id);
        Task Insert(T entity);
        Task Delete(object id);
        void Delete(T entityToDelete);
        void Update(T entityToUpdate);
        Task Save();
    }
}
