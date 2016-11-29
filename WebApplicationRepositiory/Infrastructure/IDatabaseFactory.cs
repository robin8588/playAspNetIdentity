using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApplicationDomain;

namespace WebApplicationRepositiory.Infrastructure
{
    public interface IDatabaseFactory : IDisposable
    {
        WebApplicationContext Get();
    }
}
