using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autofac;
using SparRetail.Core;
using SparRetail.Core.Config;
using SparRetail.PlugIns.Interop;

namespace SparRetail.PlugIns.MailMan.ConsoleHost
{
    class MailManHost: IHost
    {
        protected readonly IConfigCollection configCollection;
        SubscriberPlugin subscriber;

        public MailManHost(IConfigCollection configCollection)
        {
            this.configCollection = configCollection;
            subscriber = IoC.Container.Resolve<MailManPlugIn>();
        }

        public void Start()
        {
            subscriber.Start();
        }

        public void Stop()
        {
            subscriber.Stop();
        }

        
    }
}
