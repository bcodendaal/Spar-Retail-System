using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SparRetail.Core.Messaging
{
    public class ExchangeSettings
    {
        public string Key { get; set; }
        public string HostKey { get; set; }
        public string ExchangeName { get; set; }
        public string ExchangeType { get; set; }
        public bool Durable { get; set; }
        public bool AutoDelete { get; set; }
        public bool AutoCreate { get; set; }
    }
}
