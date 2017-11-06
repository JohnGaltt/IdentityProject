using Hangfire.Dashboard;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Hangfire.Annotations;
using Microsoft.Owin;

namespace _19._10._2017evening
{
    public class MyAuthorisationFilter : IDashboardAuthorizationFilter
    {
        public bool Authorize([NotNull] DashboardContext context)
        {
            var owinContext = new OwinContext(context.GetOwinEnvironment());

            // Allow all authenticated users to see the Dashboard (potentially dangerous).
            return owinContext.Authentication.User.Identity.IsAuthenticated;
        }
    }
}