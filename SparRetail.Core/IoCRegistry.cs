using Autofac;
using SparRetail.Core.Cache;
using SparRetail.Core.Config;
using SparRetail.Core.Email;
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
            builder.RegisterType<ConfigCollection>().As<IConfigCollection>().SingleInstance();
            builder.RegisterType<ConfigRepository>().As<IConfigRepository>().SingleInstance();
            builder.RegisterType<Mailer>().As<IMailer>().SingleInstance();
        }
    }
}
