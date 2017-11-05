using Autofac;
using Autofac.Integration.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace _19._10._2017evening
{
    public  class AutofacConfig
    {
      
            public static void ConfigureContainer()
            {
                var builder = new ContainerBuilder();

                // Register dependencies in controllers
                builder.RegisterControllers(typeof(MvcApplication).Assembly);

                // Register dependencies in filter attributes
                builder.RegisterFilterProvider();

                // Register dependencies in custom views
                builder.RegisterSource(new ViewRegistrationSource());

                // Register our Data dependencies
              

                var container = builder.Build();

                // Set MVC DI resolver to use our Autofac container
                DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
            
        }
    }
}