using Autofac;
using SparRetail.ApiBroker;
using SparRetail.ApiBroker.Brokers;
using SparRetail.Interop;
using SparRetail.UI.Controllers.Providers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace SparRetail.UI.Controllers
{
    public static class IoCRegistry
    {
        public static Assembly GetAssembly()
        {
            return Assembly.GetExecutingAssembly();
        }
        public static void Configure(ContainerBuilder builder)
        {
            SparRetail.Core.IoCRegistry.Configure(builder);
            
            var apiconfig = new ApiBrokerConfig() { EndPoint = "http://localhost:6837/api/" };
            builder.RegisterInstance(apiconfig).As<IApiBrokerConfig>().SingleInstance();
            builder.RegisterType<SupplierBroker>().As<ISupplierApi>().SingleInstance();
            builder.RegisterType<OrderBroker>().As<IOrderApi>().SingleInstance();
            builder.RegisterType<ProductBroker>().As<IProductApi>().SingleInstance();
            builder.RegisterType<SupplierBroker>().As<ISupplierApi>().SingleInstance();
            builder.RegisterType<RetailerBroker>().As<IRetailerApi>().SingleInstance();
            builder.RegisterType<ProfileProvider>().As<IProfileProvider>().SingleInstance();
        }
    }
}
