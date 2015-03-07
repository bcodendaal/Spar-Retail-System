using SparRetail.Interop;
using SparRetail.Models;
using SparRetail.Models.Api;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SparRetail.ApiBroker.Brokers
{
    public class RetailerBroker : ApiBrokerBase, IRetailerApi
    {
        public RetailerBroker(IApiBrokerConfig config): base(config, "retailer")
        { }

        public CreateRetailerResponse Create(Retailer retailer)
        {
            return Post<CreateRetailerResponse>("Create", retailer);
        }
    }
}
