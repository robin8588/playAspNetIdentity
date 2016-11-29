using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace WebApplicationRepositiory.Infrastructure
{
    public interface IRepository<T> where T : class
    {
        T Find(int id);
        Task<T> FindAsync(int id);
        Task<T> GetSingleOrDefaultAsync(Expression<Func<T, bool>> where);
        IQueryable<T> GetAll();
        Task<IEnumerable<T>> GetAllAsync();
        IQueryable<T> GetMany(Expression<Func<T, bool>> where);
        Task<IEnumerable<T>> GetManyAsync(Expression<Func<T, bool>> where);
        T Add(T entity);
        void Update(T entity);
        void Delete(T entity);
        bool Any(Expression<Func<T, bool>> where);
        Task<bool> AnyAsync(Expression<Func<T, bool>> where);
    }
}
