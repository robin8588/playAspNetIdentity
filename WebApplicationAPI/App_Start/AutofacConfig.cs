using Autofac;
using Autofac.Integration.WebApi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Compilation;
using System.Web.Http;
using WebApplicationRepositiory.Infrastructure;

namespace WebApplicationAPI
{
    public class AutofacConfig
    {
        public static void Config()
        {
            ConfigureContainer();
        }
        private static void ConfigureContainer()
        {
            ContainerBuilder containerBuilder = new ContainerBuilder();
            containerBuilder.RegisterApiControllers(Assembly.GetExecutingAssembly());
            containerBuilder.RegisterType<DatabaseFactory>().As<IDatabaseFactory>().InstancePerRequest();
            containerBuilder.RegisterType<UnitOfWork>().As<IUnitOfWork>().InstancePerRequest();
            var assemblys = BuildManager.GetReferencedAssemblies().Cast<Assembly>().ToList();
            containerBuilder.RegisterAssemblyTypes(assemblys.ToArray())//查找程序集中以Repository结尾的类型
            .Where(t => t.Name.EndsWith("Repository"))
            .AsImplementedInterfaces();
            IContainer container = containerBuilder.Build();
            GlobalConfiguration.Configuration.DependencyResolver = new AutofacWebApiDependencyResolver(container);
        }
    }
}