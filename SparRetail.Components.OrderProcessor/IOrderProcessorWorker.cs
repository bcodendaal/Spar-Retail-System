using SparRetail.Models.Api;
using SparRetail.Models.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SparRetail.Components.OrderProcessor
{
    public interface IOrderProcessorWorker
    {
        ResponseModel FinalizeOrder(FinalizeOrderCommand command);
    }
}
