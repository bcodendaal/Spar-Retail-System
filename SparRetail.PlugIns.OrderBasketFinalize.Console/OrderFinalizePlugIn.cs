using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using SparRetail.Core.Messaging;
using SparRetail.Orders.Services;
using SparRetail.Components.OrderProcessor;
using SparRetail.Models.Commands;

namespace SparRetail.PlugIns.OrderBasketFinalize.ConsoleHost
{
    public class OrderFinalizePlugIn : SubscriberPlugin
    {
        protected readonly IOrderProcessorWorker OrderService;
        

        public OrderFinalizePlugIn(IOrderProcessorWorker orderService, ExchangeSettings exchangeSettings, HostSettings hostSettings, QueueSettings queueSettings)
            : base(exchangeSettings, hostSettings, queueSettings)
        {
            this.OrderService = orderService;
        }

        protected override ProcessMessageResult ProcessMessage(string message, IDictionary<string, object> headers)
        {
            try
            {
                var response = OrderService.FinalizeOrder(JsonConvert.DeserializeObject<FinalizeOrderCommand>(message));
                if (response.IsCallSuccess && response.IsCommandSuccess)
                    return ProcessMessageResult.Success;
                else
                    return ProcessMessageResult.Retry;
            }
            catch (JsonReaderException jsonEx)
            {
                Logger.Error(jsonEx);
                return ProcessMessageResult.Failed;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                return ProcessMessageResult.Retry;
            }
        }
    }
}
