using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace WebApplicationAPI
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API 配置和服务
            var json = config.Formatters.JsonFormatter;
            json.SerializerSettings.DateFormatString = "yyyy-MM-dd HH:mm:ss";
            // Web API 路由
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}
