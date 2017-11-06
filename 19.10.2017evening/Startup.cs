using _19._10._2017evening.Models;
using Autofac;
using Autofac.Integration.Mvc;
using Hangfire;
using Hangfire.Dashboard;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using Owin;
using System.Web.Mvc;

[assembly: OwinStartupAttribute(typeof(_19._10._2017evening.Startup))]
namespace _19._10._2017evening
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            var builder = new ContainerBuilder();
            // Register dependencies, then...
           

            // Register the Autofac middleware FIRST. This also adds
            // Autofac-injected middleware registered with the container.
            
            builder.RegisterControllers(typeof(MvcApplication).Assembly);
            
            var container = builder.Build();
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
            app.UseAutofacMiddleware(container);

            builder.RegisterType<ApplicationUser>().As<IdentityUser>();
            builder.RegisterType<ApplicationDbContext>().As<IdentityDbContext<ApplicationUser>>();
            builder.RegisterType<ApplicationRole>().As<IdentityRole>();
            builder.RegisterType<ApplicationSignInManager>().As<SignInManager<ApplicationUser, string>>();

               ConfigureAuth(app);
            GlobalConfiguration.Configuration.UseSqlServerStorage("DefaultConnection");

           // app.UseHangfireDashboard();
            app.UseHangfireDashboard("/hangfire", new DashboardOptions
            {
                Authorization = new[] { new MyAuthorisationFilter() }
            });

            app.UseHangfireServer();

           // app.MapHangfireDashboard("/hangfire", new[] { HangFireAuthorizationFilter() })
        }
    }
}
