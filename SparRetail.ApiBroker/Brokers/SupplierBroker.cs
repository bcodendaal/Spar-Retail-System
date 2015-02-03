using SparRetail.Interop;
using SparRetail.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SparRetail.ApiBroker.Brokers
{
    public class SupplierBroker : ApiBrokerBase, ISupplierApi
    {
        public SupplierBroker(IApiBrokerConfig config)
            : base(config)
        { }

        public List<Supplier> All()
        {
            return Get<List<Supplier>>("All");
        }
    }
}
