using System.Web.Http;
using System.Web.Http.Cors;

namespace Presentation
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {           
            // Web API routes
            config.MapHttpAttributeRoutes();
            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{action}/{id}/{id2}",
                defaults: new { id = RouteParameter.Optional,id2 = RouteParameter.Optional, action = RouteParameter.Optional }
            );
        }
    }
}
