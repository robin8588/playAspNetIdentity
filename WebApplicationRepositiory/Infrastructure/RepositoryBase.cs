using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using WebApplicationDomain;

namespace WebApplicationRepositiory.Infrastructure
{
    public abstract class RepositoryBase<T> : IRepository<T> where T : class, new()
    {
        private WebApplicationContext dataContext;
        private readonly DbSet<T> dbset;
        protected RepositoryBase(IDatabaseFactory databaseFactory)
        {
            DatabaseFactory = databaseFactory;
            dbset = DataContext.Set<T>();
        }
        protected IDatabaseFactory DatabaseFactory
        {
            get;
            private set;
        }
        protected WebApplicationContext DataContext
        {
            get { return dataContext ?? (dataContext = DatabaseFactory.Get()); }
        }
        public virtual T Add(T entity)
        {
            dbset.Add(entity);
            return entity;
        }
        public virtual void Update(T entity)
        {
            dbset.Attach(entity);
            dataContext.Entry(entity).State = EntityState.Modified;
        }
        public virtual void Delete(T entity)
        {
            dbset.Remove(entity);
        }
        public virtual void Delete(Expression<Func<T, bool>> where)
        {
            IEnumerable<T> objects = dbset.Where<T>(where).AsEnumerable();
            foreach (T obj in objects)
                dbset.Remove(obj);
        }
        public virtual T Find(int id)
        {
            return dbset.Find(id);
        }
        public virtual async Task<T> FindAsync(int id)
        {
            return await dbset.FindAsync(id);
        }
        public virtual async Task<T> GetSingleOrDefaultAsync(Expression<Func<T, bool>> where)
        {
            return await dbset.SingleOrDefaultAsync(where);
        }
        public virtual IQueryable<T> GetAll()
        {
            return dbset;
        }
        public virtual async Task<IEnumerable<T>> GetAllAsync()
        {
            return await dbset.ToListAsync();
        }
        public virtual IQueryable<T> GetMany(Expression<Func<T, bool>> where)
        {
            return dbset.Where(where);
        }
        public virtual async Task<IEnumerable<T>> GetManyAsync(Expression<Func<T, bool>> where)
        {
            return await dbset.Where(where).ToListAsync();
        }
        public virtual bool Any(Expression<Func<T, bool>> where)
        {
            return dbset.Any(where);
        }
        public virtual async Task<bool> AnyAsync(Expression<Func<T, bool>> where)
        {
            return await dbset.AnyAsync(where);
        }
    }
}
