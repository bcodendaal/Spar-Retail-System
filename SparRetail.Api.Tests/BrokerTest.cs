using NUnit.Framework;
using SparRetail.ApiBroker;
using SparRetail.ApiBroker.Brokers;
using SparRetail.Models;
using SparRetail.Models.Api;
using System;
using System.Collections.Generic;
using System.Configuration;
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
            SupplierBroker broker = new SupplierBroker(new ApiBrokerConfig { EndPoint = ConfigurationManager.AppSettings["apiEndpoint"] });
            var suppliers = broker.All();
            Assert.IsNotNull(suppliers);
            Assert.IsTrue(suppliers.Count == 2);
        }

        [TestCase]
        public void broker_successfully_returns_products()
        {
            ProductBroker broker = new ProductBroker(new ApiBrokerConfig { EndPoint = ConfigurationManager.AppSettings["apiEndpoint"] });
            var products = broker.GetAllForSupplier(new Supplier { SupplierId = 2, DatabaseConfigKey = "dbSupplier" });
            Assert.IsNotNull(products);
            Assert.IsTrue(products.Count > 1);
        }

        [TestCase]
        public void broker_order_getallforretailer()
        {
            OrderBroker broker = new OrderBroker(new ApiBrokerConfig { EndPoint = ConfigurationManager.AppSettings["apiEndpoint"] });
            var orderBaskets = broker.AllOrderBasketForRetailer(1);
            Assert.IsNotNull(orderBaskets);
            Assert.IsTrue(orderBaskets.Count > 1);
        }

        [TestCase]
        public void add_order_basket_item()
        {
            OrderBroker broker = new OrderBroker(new ApiBrokerConfig { EndPoint = ConfigurationManager.AppSettings["apiEndpoint"] });
            var result = broker.AddOrderBasketItem(new OrderBasketItemPost
            {
                RetailerId = 1,
                OrderBasketItem = new OrderBasketItem 
            {
                OrderBasketId = 1,
                ProductId = 1,
                BarCode = "ABCD",
                ProductCode = "MILK",
                ProductName = "2L Milk",
                NumberOfUnits = 2,
                PricePerUnit = 15,
                UnitOfMeasure = "Bottle",
                TotalPrice = 30                
            }
            });

            
            Assert.NotNull(result);
            Assert.IsTrue(result.IsCommandSuccess);
            Assert.IsTrue(result.IsCallSuccess);
        }
    }
}
