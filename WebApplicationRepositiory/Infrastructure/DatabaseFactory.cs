using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApplicationDomain;

namespace WebApplicationRepositiory.Infrastructure
{
    public class DatabaseFactory : Disposable, IDatabaseFactory
    {
        private WebApplicationContext dataContext;
        public WebApplicationContext Get()
        {
            return dataContext ?? (dataContext = new WebApplicationContext());
        }
        protected override void DisposeCore()
        {
            if (dataContext != null)
            {
                dataContext.Dispose();
            }
        }
    }
}
