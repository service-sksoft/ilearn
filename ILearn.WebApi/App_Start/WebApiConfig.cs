﻿namespace ILearn.WebApi
    {
    using System.Web.Http;
    using System.Web.Http.Cors;

    public static class WebApiConfig
        {
        public static void Register(HttpConfiguration config)
            {
            var cors = new EnableCorsAttribute("http://ilearn.net.in,http://localhost:5555", "*", "*");

            config.EnableCors(cors);

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            config.Routes.MapHttpRoute(
            name: "DefaultApiWithAction",
            routeTemplate: "api/{controller}/{action}/{id}",
            defaults: new { id = RouteParameter.Optional }
            );
            }
        }
    }
