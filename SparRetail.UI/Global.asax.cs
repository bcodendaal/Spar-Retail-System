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
using SparRetail.UI.Controllers.Providers;
using SparRetail.UI.Controllers;
using SparRetail.Core.Config;


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
             
                //builder.RegisterInstance(apiconfig).As<IApiBrokerConfig>().SingleInstance();
                //builder.RegisterType<SupplierBroker>().As<ISupplierApi>().SingleInstance();
                //builder.RegisterType<OrderBroker>().As<IOrderApi>().SingleInstance();
                //builder.RegisterType<ProductBroker>().As<IProductApi>().SingleInstance();
                //builder.RegisterType<SupplierBroker>().As<ISupplierApi>().SingleInstance();

                var config = GetConfigCollection();
                IoCRegistry.Configure(builder, config);
                builder.RegisterControllers(SparRetail.UI.Controllers.IoCRegistry.GetAssembly());
                //builder.RegisterControllers(typeof(MvcApplication).Assembly);

                // builder.RegisterFilterProvider();
            });

            DependencyResolver.SetResolver(new AutofacDependencyResolver(IoC.Container));
            log4net.Config.XmlConfigurator.Configure();
        }

        private IConfigCollection GetConfigCollection()
        {
            return new ConfigCollection(new ConfigRepository());
        }
    }
}
