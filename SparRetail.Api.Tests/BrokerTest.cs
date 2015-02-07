using Microsoft.VisualStudio.TestTools.UnitTesting;
using SparRetail.ApiBroker;
using SparRetail.ApiBroker.Brokers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SparRetail.Api.Tests
{
    [TestClass]
    public class BrokerTest
    {
        [TestMethod]
        public void broker_successfully_returns_suppliers()
        {
            SupplierBroker broker = new SupplierBroker( new ApiBrokerConfig { ControllerSegment = "supplier", EndPoint = "http://localhost:6837/api/" });
            var suppliers = broker.All();
            Assert.IsNotNull(suppliers);
            Assert.IsTrue(suppliers.Count == 2);
        }
    }
}
