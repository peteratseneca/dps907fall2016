using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace LinkRelationsIntro
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services

            // Attention 11 - Add the HTTP OPTIONS handler to the request-processing pipeline
            config.MessageHandlers.Add(new ServiceLayer.HandleHttpOptions());

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
