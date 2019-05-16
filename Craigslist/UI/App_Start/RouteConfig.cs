using System.Web.Mvc;
using System.Web.Routing;


namespace UI
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Admin-Location",
                url: "Location/{area}/ListLocale",
                defaults: new { controller = "Locations", action = "ListLocale"}
            );

            routes.MapRoute(
                name: "Homepage",
                url: "Home/Index/{area}/{locale}/{category}/{sub}",
                defaults: new { controller = "Home", action = "Index",
                    area = "New York", category = UrlParameter.Optional,
                    locale = UrlParameter.Optional, sub = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
