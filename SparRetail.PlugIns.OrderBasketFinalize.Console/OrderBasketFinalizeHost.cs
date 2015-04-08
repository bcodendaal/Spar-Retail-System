using SparRetail.Core.Config;
using SparRetail.Models.Commands;
using SparRetail.PlugIns.Interop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SparRetail.Core;
using Autofac;

namespace SparRetail.PlugIns.OrderBasketFinalize.ConsoleHost
{
    public class OrderBasketFinalizeHost : IHost
    {
        protected readonly IConfigCollection configCollection;
        SubscriberPlugin subscriber;

        public OrderBasketFinalizeHost(IConfigCollection configCollection)
        {
            this.configCollection = configCollection;
            subscriber = IoC.Container.Resolve<OrderFinalizePlugIn>();
        }

        public void Start()
        {
            subscriber.Start();
        }

        public void Stop()
        {
            subscriber.Stop();
        }

        public void Handle(FinalizeOrderCommand command)
        {
            System.Console.WriteLine(command);
        }
    }
}
