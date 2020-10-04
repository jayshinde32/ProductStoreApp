using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.Http.ExceptionHandling;
using WebApi.Filter;

namespace WebApi
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services

            // Web API routes          
            config.MapHttpAttributeRoutes();
            config.Services.Replace(typeof(IExceptionHandler), new ExceptionLogFilter());
            config.Services.Replace(typeof(IExceptionLogger), new ExceptionLog());
            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}
