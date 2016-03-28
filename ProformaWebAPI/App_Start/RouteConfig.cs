using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Routing;

namespace ProformaWebAPI
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );

            //    routes.MapRoute(
            //    name: "ApiById",
            //    url: "api/{controller}/{id}",
            //    defaults: new { id = RouteParameter.Optional },
            //    constraints: new { id = @"^[0-9]+$" }
            //);

            //    routes.MapRoute(
            //        name: "ApiByName",
            //        url: "api/{controller}/{action}/{name}",
            //        defaults: null,
            //        constraints: new { name = @"^[a-z]+$" }
            //    );

            //    routes.MapRoute(
            //        name: "ApiByAction",
            //        url: "api/{controller}/{action}",
            //        defaults: new { action = "Get" }
            //    );

            routes.MapHttpRoute(
                name: "ActionApi",
                routeTemplate: "api/{controller}/{action}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}
