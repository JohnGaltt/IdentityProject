using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Autofac;
using Autofac.Integration.Mvc;
using _19._10._2017evening.Controllers;

namespace _19._10._2017evening
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {

  

            // Register your MVC controllers. (MvcApplication is the name of
            // the class in Global.asax.)
            AutofacConfig.ConfigureContainer();
      
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
    }
}
