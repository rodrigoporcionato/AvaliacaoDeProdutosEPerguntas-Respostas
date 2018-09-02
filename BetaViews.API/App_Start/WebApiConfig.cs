using BetaViews.API.Filters;
using System.Web.Http;

namespace BetaViews.API
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {

            // Web API configuration and services

            //GlobalConfiguration.Configuration.Filters.Add(new APIAuthenticationAttribute());

            //config.Filters.Add(new APIAuthenticationAttribute());

            //config.EnableCors();

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            config.Formatters.Remove(config.Formatters.XmlFormatter);

            
        }
    }
}
