using NServiceBus;
using SparRetail.Models.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SparRetail.PlugIns.OrderBasketFinalize.Console
{
    public class OrderBasketFinalizeHandler : IHandleMessages<FinalizeOrderCommand>
    {
        public IBus Bus { get; set; }

        public void Handle(FinalizeOrderCommand command)
        {
            System.Console.WriteLine(command);
        }
    }
}
