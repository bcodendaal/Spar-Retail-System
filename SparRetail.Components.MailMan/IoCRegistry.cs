using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autofac;
using SparRetail.Core.Config;

namespace SparRetail.Components.MailMan
{
    public static class IoCRegistry
    {
        public static void Configure(ContainerBuilder builder, IConfigCollection config)
        {
            SparRetail.Core.IoCRegistry.Configure(builder, config);
            builder.RegisterType<MailManWorker>().As<IMailManWorker>().SingleInstance();
        }
    }
}
