using Autofac;
using SparRetail.DatabaseConfigAdapter.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SparRetail.DatabaseConfigAdapter
{
    public static class IoCRegistry
    {
        public static void Configure(ContainerBuilder builder)
        {
            builder.RegisterType<DatabaseConfigAdapter>().As<IDatabaseConfigAdapter>().SingleInstance();
            builder.RegisterType<DatabaseConfigAdapterRepository>().As<IDatabaseConfigAdapterRepository>().SingleInstance();
        }
    }
}
