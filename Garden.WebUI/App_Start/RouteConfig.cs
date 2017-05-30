using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Garden.WebUI
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
               name: "",
               url: "{category}/{page}",
               defaults: new { controller = "Category", action = "Index" },
               constraints: new { page=@"\d+"}
           );

            routes.MapRoute(
                name: "",
                url: "{controller}/{action}/{category}",
                defaults: null
            );


            //Установка стандартного маршрута
            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );

            routes.MapRoute(null, "{controller}/{action}");
        }
    }
}