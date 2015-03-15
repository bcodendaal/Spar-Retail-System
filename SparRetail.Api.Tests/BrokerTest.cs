using NUnit.Framework;
using SparRetail.ApiBroker;
using SparRetail.ApiBroker.Brokers;
using SparRetail.Models;
using SparRetail.Models.Api;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
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

        [TestCase]
        public void get_items_for_order_basket()
        {
            OrderBroker broker = new OrderBroker(new ApiBrokerConfig { EndPoint = ConfigurationManager.AppSettings["apiEndpoint"] });
            var result = broker.AllItemsForOrderBasket(1, 1);

            Assert.NotNull(result);
            Assert.True(result.Count > 0);
        }

        [TestCase]
        public void finalise_order_successfully()
        {
            OrderBroker broker = new OrderBroker(new ApiBrokerConfig { EndPoint = ConfigurationManager.AppSettings["apiEndpoint"] });
            var result = broker.FinaliseOrder(new FinaliseOrderPost { OrderBasketId = 34, RetailerId = 1 });

            Assert.NotNull(result);
            Assert.IsTrue(result.IsCallSuccess);
            Assert.IsTrue(result.IsCommandSuccess);
        }

        [TestCase]
        public void add_retailer_successfully()
        {
            RetailerBroker broker = new RetailerBroker(new ApiBrokerConfig { EndPoint = ConfigurationManager.AppSettings["apiEndpoint"] });
            var result = broker.Create(new Retailer 
            {
                RetailerCode = "SBC",
                VatNumber = "0981234156",
                StoreCode = "SBC1",
                RetailerName = "Stellenbosch Brewing Company",
                Email = "info@sbc.co.za",
                AddressLine1 = "123 Merriman Road",
                AddressLine2 = "Stellenbosch",
                AddressLine3 = "",
                City = "Cape Town",
                Fax = "021 908 1231",
                PostalCode = "9088",
                Province = "Western Cape",
                Telephone = "021 987 1231"                
            });

            Debug.WriteLine(result.Message);
            Assert.NotNull(result);
            Assert.IsTrue(result.IsCallSuccess);
            Assert.IsTrue(result.IsCommandSuccess);


        }
    }
}
