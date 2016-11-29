using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApplicationDomain;

namespace WebApplicationRepositiory.Infrastructure
{
    public class UnitOfWork : IUnitOfWork
    {
        private WebApplicationContext dbContext;
        private readonly IDatabaseFactory databaseFactory;
        protected WebApplicationContext DBContext
        {
            get
            {
                return dbContext ?? databaseFactory.Get();
            }
        }
        public UnitOfWork(IDatabaseFactory databaseFactory)
        {
            this.databaseFactory = databaseFactory;
            this.dbContext = this.databaseFactory.Get();
        }
        public void SaveChanges()
        {
            DBContext.SaveChanges();
        }
        public Task SaveChangesAsync()
        {
            return DBContext.SaveChangesAsync();
        }
    }
}
