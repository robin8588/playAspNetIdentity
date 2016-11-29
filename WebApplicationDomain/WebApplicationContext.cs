using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApplicationDomain.Migrations;

namespace WebApplicationDomain
{
    public class WebApplicationContext:DbContext
    {
        public WebApplicationContext() : base("WebApplicationConnection")
        {

        }

        static WebApplicationContext()
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<WebApplicationContext, Configuration>());
        }
    }
}
