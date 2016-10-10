using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
// added...
using System.Web.Http.ExceptionHandling;

namespace HypermediaObjectGraph
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services

            // Attention 21 - Added CORS support, to enable another code example to work
            // Read more about CORS support here...
            // https://www.asp.net/web-api/overview/security/enabling-cross-origin-requests-in-web-api#enable-cors
            // The NuGet Package Manager was used to add support to this project
            // Then, we enable it here... and can activate it per controller and/or method (or for the whole project)

            config.EnableCors();

            config.MessageHandlers.Add(new ServiceLayer.HandleHttpOptions());

            // This is the customized error handler
            config.Services.Replace(typeof(IExceptionHandler), new ServiceLayer.HandleError());

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { controller = "Root", id = RouteParameter.Optional }
            );
        }
    }
}
