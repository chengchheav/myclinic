using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using MyClinic.Core;
using MyClinic.Infrastructure;

namespace MyClinic
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
                "{lang}/{controller}/{action}/{id}", // URL with parameters
                new { lang="en", controller = "Home", action = "Index", id = UrlParameter.Optional } // Parameter defaults
            );

            routes.MapRoute(
                "LinkPage", // Route name
                "{lang}/{controller}/{action}/{id}", // URL with parameters
                new { lang = "en", controller = "Home", action = "Index", id = UrlParameter.Optional } // Parameter defaults
            );

        }

        protected void Application_Start()
        {
            //Log NHibernate statement in log4net
            log4net.Config.XmlConfigurator.Configure();
            //Utilities.Common.host = HttpRuntime.AppDomainAppVirtualPath == "/" ? "" : HttpRuntime.AppDomainAppVirtualPath;
            AreaRegistration.RegisterAllAreas();
            RegisterGlobalFilters(GlobalFilters.Filters);
            RegisterRoutes(RouteTable.Routes);

            BackupDbTimer backupDbTimer = new BackupDbTimer();
            backupDbTimer.Interval = 5;//5 Minutes
            backupDbTimer.Start();
        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {
            if (Request.Url.ToString().Contains("/en") || Request.Url.ToString().Contains("/km"))
            {
                Utilities.Common.ChangeLanguage(Request.Url.Segments);
                //Set default language cookies
                var lang = Request.Url.Segments[1];
                var cleanLang=lang.Replace("/", "");
                Utilities.Common.host = Request.Url.Scheme + "://" + Request.Url.Authority + Request.ApplicationPath.TrimEnd('/');
                HttpCookie myCookie = new HttpCookie("MyShopCookie");
                DateTime now = DateTime.Now;
                myCookie.Values["lang"] = lang;
                myCookie.Expires = now.AddYears(1);
                Response.Cookies.Add(myCookie);
            }
        }

        protected void Session_Start(Object sender, EventArgs e)
        {

        }
    }
}