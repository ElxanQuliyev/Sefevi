using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace Sefevi
{
    public class MvcApplication : System.Web.HttpApplication
    {
        const string DefaultCulture = "az";
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
        public static void SetCulture(string name)
        {
            System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo(name);
            System.Threading.Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo(name);
        }
        protected void Application_BeginRequest(object sender, EventArgs e)
        {
            var language = HttpContext.Current.Request.Cookies.Language() ?? DefaultCulture;
            MainLanguage.lb = language;
            SetCulture(language);
        }
    }
}
