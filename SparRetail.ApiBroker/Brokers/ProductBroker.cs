using SparRetail.Interop;
using SparRetail.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SparRetail.ApiBroker.Brokers
{
    public class ProductBroker : ApiBrokerBase, IProductApi
    {
        public ProductBroker(IApiBrokerConfig config) : base(config, "product")
        {

        }

        public List<Product> GetAllForSupplier(Supplier supplier)
        {
            return Post<List<Product>>("GetAllForSupplier", supplier);
        }
        public Page<Product> GetSupplierProductsPaged(ProductPagedParams page)
        {
            return Post<Page<Product>>("GetSupplierProductsPaged", page);
        }
    }
}
