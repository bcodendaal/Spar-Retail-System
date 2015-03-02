using Autofac;
using SparRetail.Retailers.Repositories;
using SparRetail.Retailers.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SparRetail.Retailers
{
    public static class IoCRegistry
    {
        public static void Configure(ContainerBuilder builder)
        {
            builder.RegisterType<RetailerRepository>().As<IRetailerRepository>().SingleInstance();
            builder.RegisterType<RetailerService>().As<IRetailerService>().SingleInstance();
            DatabaseConfigAdapter.IoCRegistry.Configure(builder);

        }
    }
}
