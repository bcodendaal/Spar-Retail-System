using NServiceBus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SparRetail.Models.Commands
{
    public class FinalizeOrderCommand : ICommand
    {
        public int OrderBasketId { get; set; }
        public int RetailerId { get; set; }
    }
}
