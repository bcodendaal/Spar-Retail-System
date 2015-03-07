using Autofac;
using SparRetail.Orders.Repositories;
using SparRetail.Orders.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SparRetail.Orders
{
    public static class IoCRegistry
    {
        public static void Configure(ContainerBuilder builder)
        {
            builder.RegisterType<OrderBasketRepository>().As<IOrderBasketRepository>().SingleInstance();
            builder.RegisterType<OrderBasketService>().As<IOrderBasketService>().SingleInstance();

            builder.RegisterType<OrderService>().As<IOrderService>().SingleInstance();
            builder.RegisterType<OrderRepository>().As<IOrderRepository>().SingleInstance();

            Suppliers.IoCRegistry.Configure(builder);
            Retailers.IoCRegistry.Configure(builder);
            DatabaseConfigAdapter.IoCRegistry.Configure(builder);
        }
    }
}
