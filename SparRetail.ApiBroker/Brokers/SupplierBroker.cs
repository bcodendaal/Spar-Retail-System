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
    public class SupplierBroker : ApiBrokerBase, ISupplierApi
    {
        public SupplierBroker(IApiBrokerConfig config)
            : base(config, "supplier")
        { }

        public List<Supplier> All()
        {
            return Get<List<Supplier>>("All");
        }


        public CreateSupplierResponse Create(Supplier supplier)
        {
            return Post<CreateSupplierResponse>("Create", supplier);
        }
    }
}
