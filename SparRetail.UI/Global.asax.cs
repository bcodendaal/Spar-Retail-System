using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Autofac;
using Autofac.Integration.Mvc;
using SparRetail.ApiBroker;
using SparRetail.ApiBroker.Brokers;
using SparRetail.Interop;


namespace Spar.Retail.UI
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            IoC.BootStrap(builder =>
            {
                SparRetail.UI.Controllers.IoCRegistry.Configure(builder);
                builder.RegisterControllers(SparRetail.UI.Controllers.IoCRegistry.GetAssembly());
                //builder.RegisterControllers(typeof(MvcApplication).Assembly);
                
               // builder.RegisterFilterProvider();
            });

            DependencyResolver.SetResolver(new AutofacDependencyResolver(IoC.Container));

        }
    }
}
