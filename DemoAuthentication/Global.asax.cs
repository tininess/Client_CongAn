using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Security;
using System.Security.Principal;

namespace DemoAuthentication
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
                "{controller}/{action}/{id}", // URL with parameters
                new { controller = "Home", action = "Index", id = UrlParameter.Optional } // Parameter defaults
            );

        }

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            RegisterGlobalFilters(GlobalFilters.Filters);
            RegisterRoutes(RouteTable.Routes);
        }
        //protected void Application_AuthenticateRequest(object sender, EventArgs e)
        //{
        //    if (HttpContext.Current.User != null)
        //    {
        //        if (HttpContext.Current.User.Identity.IsAuthenticated)
        //        {
        //            if (HttpContext.Current.User.Identity is FormsIdentity)
        //            { 
        //                FormsIdentity id =
        //                    (FormsIdentity)HttpContext.Current.User.Identity;
        //                FormsAuthenticationTicket ticket = id.Ticket;

        //                // Get the stored user-data, in this case, our roles
        //                string userData = ticket.UserData;
        //                string[] roles = userData.Split(',');
        //                HttpContext.Current.User = new GenericPrincipal(id, roles);
        //            }
        //        }
        //    }
        //}

    }
}