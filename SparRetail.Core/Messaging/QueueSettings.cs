using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SparRetail.Core.Messaging
{
    public class QueueSettings
    {
        public string QueueName { get; set; }
        public int MessageTTL { get; set; }
        public string RoutingKey { get; set; }
    }
}
