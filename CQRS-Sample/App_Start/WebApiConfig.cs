using CQRS_Sample.Infrastructure.Autofac;
using System.Web;
using System.Web.Http;

namespace CQRS_Sample
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services

            IoC.Configure(config, HttpRuntime.BinDirectory);
            
            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}
