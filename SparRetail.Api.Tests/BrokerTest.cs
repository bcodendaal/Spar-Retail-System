using NUnit.Framework;
using SparRetail.ApiBroker;
using SparRetail.ApiBroker.Brokers;
using SparRetail.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SparRetail.Api.Tests
{
    [TestFixture]
    public class BrokerTest
    {
        [TestCase]
        public void broker_successfully_returns_suppliers()
        {
            SupplierBroker broker = new SupplierBroker( new ApiBrokerConfig { EndPoint = "http://localhost:6837/api/" });
            var suppliers = broker.All();
            Assert.IsNotNull(suppliers);
            Assert.IsTrue(suppliers.Count == 2);
        }

        [TestCase]
        public void broker_successfully_returns_products()
        {
            ProductBroker broker = new ProductBroker(new ApiBrokerConfig { EndPoint = "http://localhost:6837/api/" });
            var products = broker.GetAllForSupplier(new Supplier { SupplierId = 2, DatabaseConfigKey = "dbSupplier" });
            Assert.IsNotNull(products);
            Assert.IsTrue(products.Count > 1);
        }
    }
}
