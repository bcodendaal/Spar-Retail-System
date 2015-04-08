using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SparRetail.Core.Messaging
{
    public interface IMessageProducer
    {
        void PublishMessage(string exchangeSettingsKey, List<object> messages, string routingKey, Dictionary<string, object> headers = null);
    }
}
