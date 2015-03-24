using MassTransit;
using MassTransit.Log4NetIntegration.Logging;
using SparRetail.Core.Config;
using SparRetail.Models.Commands;
using SparRetail.PlugIns.Interop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SparRetail.PlugIns.OrderBasketFinalize.ConsoleHost
{
    public class OrderBasketFinalizeHost : IHost
    {
        public IServiceBus Bus;
        protected readonly IConfigCollection configCollection;

        public OrderBasketFinalizeHost(IConfigCollection configCollection)
        {
            this.configCollection = configCollection;
        }

        public void Start()
        {
            Bus = ServiceBusFactory.New(x => 
            {
                Log4NetLogger.Use();
                x.UseRabbitMq();
                x.ReceiveFrom(string.Format("rabbitmq://{0}/{1}", configCollection.Get(SharedConfigKeys.RabbitHost), "orderbasket.finalize.consumer")); 
                x.Subscribe(subs => 
                {
                    subs.Consumer<OrderBasketFinalizeHandler>().Permanent();
                });
            });
        }

        public void Stop()
        {
            Bus.Dispose();
        }

        public void Handle(FinalizeOrderCommand command)
        {
            System.Console.WriteLine(command);
        }
    }
}
