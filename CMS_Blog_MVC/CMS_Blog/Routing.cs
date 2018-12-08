using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;

namespace CMS_Blog
{
    public class Routing
    {
        public static IRouteBuilder ConfigureRoutes(IRouteBuilder routeBuilder)
        {
            AddDefaultRoute(routeBuilder, defaultController: "Home", defaultAction: "Index");
            return routeBuilder;
        }

        private static void AddDefaultRoute(IRouteBuilder routeBuilder, string defaultController, string defaultAction)
        {
            routeBuilder.MapRoute(
                name: "default",
                template: "{controller}/{action}/{id?}",
                defaults: new { controller = defaultController, action = defaultAction });

            routeBuilder.MapRoute(
                name: "DefaultApi",
                template: "{controller}/{action}",
                defaults: new { controller = "Home", action = "GetUser" });

            routeBuilder.MapRoute(
                name: "Backend",
                template: "{controller}/{action}",
                defaults: new { controller = "Home", action = "GetBackend" });

            routeBuilder.MapRoute(
                name: "Login",
                template: "{controller}/{action}",
                defaults: new { controller = "Home", action = "Login" });
        }
    }
}