using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace UtahPlanners.MVC3
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : System.Web.HttpApplication
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }

        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                "Default", // Route name
                "{action}/{id}", // URL with parameters
                new { controller = "Home", action = "Default", id = UrlParameter.Optional }, // Parameter defaults
                new { defaultConstraint = new DefaultController() }
                //,new { id = @"\d+" }
            );

            routes.MapRoute(
                "Admin",
                "{controller}/{action}/{id}",
                new { id = UrlParameter.Optional }
            );
        }

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            RegisterGlobalFilters(GlobalFilters.Filters);
            RegisterRoutes(RouteTable.Routes);
        }
    }

    public class DefaultController : IRouteConstraint
    {

        #region IRouteConstraint Members

        public bool Match(HttpContextBase httpContext, Route route, string parameterName, RouteValueDictionary values, RouteDirection routeDirection)
        {
            List<string> defaultActions = new List<string>
            {
                "Default",
                "Index",
                "About",
                "Property"
            };
            return defaultActions.Contains(values["action"].ToString());
        }

        #endregion
    }
}