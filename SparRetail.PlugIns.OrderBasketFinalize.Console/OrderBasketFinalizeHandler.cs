using MassTransit;
using SparRetail.Components.OrderProcessor;
using SparRetail.Core;
using SparRetail.Models.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Autofac;
using System.Threading.Tasks;

namespace SparRetail.PlugIns.OrderBasketFinalize.ConsoleHost
{
    public class OrderBasketFinalizeHandler : Consumes<FinalizeOrderCommand>.Context
    {
        protected IOrderProcessorWorker worker;

        public OrderBasketFinalizeHandler()
        {
            worker = IoC.Container.Resolve<IOrderProcessorWorker>();
        }

        public void Consume(IConsumeContext<FinalizeOrderCommand> context)
        {
            var result = worker.FinalizeOrder(context.Message);
            if (!result.IsCommandSuccess)
                throw new Exception(result.Message);
        }
    }
}
