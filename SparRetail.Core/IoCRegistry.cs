using Autofac;
using SparRetail.Core.Cache;
using SparRetail.Core.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SparRetail.Core
{
    public static class IoCRegistry
    {
        public static void Configure(ContainerBuilder builder)
        {
            builder.RegisterType<InMemoryCacheRepository>().As<ICacheRepository>().SingleInstance();
            builder.RegisterType<CacheBroker>().As<ICacheBroker>().SingleInstance();
            builder.RegisterType<Logger>().As<ILogger>().SingleInstance();
        }
    }
}
