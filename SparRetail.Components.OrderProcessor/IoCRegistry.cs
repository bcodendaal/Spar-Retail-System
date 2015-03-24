using Autofac;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SparRetail.Components.OrderProcessor
{
    public static class IoCRegistry
    {
        public static void Configure(ContainerBuilder builder)
        {
            builder.RegisterType<OrderProcessorWorker>().As<IOrderProcessorWorker>().SingleInstance();
            builder.RegisterType<OrderProcessWorkerRepository>().As<IOrderProcessorWorkerRepository>().SingleInstance();
            SparRetail.Core.IoCRegistry.Configure(builder);
            SparRetail.Orders.IoCRegistry.Configure(builder);
            SparRetail.DatabaseConfigAdapter.IoCRegistry.Configure(builder);
            SparRetail.Retailers.IoCRegistry.Configure(builder);
            SparRetail.Suppliers.IoCRegistry.Configure(builder);

        }
    }
}
