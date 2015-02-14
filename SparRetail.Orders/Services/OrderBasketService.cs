using SparRetail.Core.Logging;
using SparRetail.DatabaseConfigAdapter;
using SparRetail.Models;
using SparRetail.Orders.Repositories;
using SparRetail.Retailers.Services;
using SparRetail.Suppliers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SparRetail.Orders.Services
{
    public class OrderBasketService : IOrderBasketService
    {
        protected readonly IOrderBasketRepository orderBasketRepository;
        protected readonly IDatabaseConfigAdapter databaseConfigAdapter;
        protected readonly ISupplierService supplierService;
        protected readonly IRetailerService retailerService;
        protected readonly ILogger logger;


        private const string TagGroup = "OrderBasketService";

        public OrderBasketService(IOrderBasketRepository orderBasketRepository, IDatabaseConfigAdapter databaseConfigAdapter, ISupplierService supplierService, IRetailerService retailerService, ILogger logger)
        {
            this.orderBasketRepository = orderBasketRepository;
            this.databaseConfigAdapter = databaseConfigAdapter;
            this.supplierService = supplierService;
            this.retailerService = retailerService;
            this.logger = logger;
        }


        public List<OrderBasket> AllForRetailerSupplier(int retailerId, int supplierId)
        {
            var baskets = orderBasketRepository.AllForRetailerSupplier(retailerId, databaseConfigAdapter.GetRetailerDatabaseConfigKey(retailerId), supplierId);

            if (baskets != null && baskets.Any())
            {
                baskets.ForEach(x =>
                {
                    x.Retailer = retailerService.GetById(x.RetailerId);
                    x.Supplier = supplierService.GetById(x.SupplierId);
                });
            }

            return baskets;
        }

        public List<OrderBasket> AllForRetailer(int retailerId)
        {
            var baskets = orderBasketRepository.AllForRetailer(retailerId, databaseConfigAdapter.GetRetailerDatabaseConfigKey(retailerId));
            if (baskets != null && baskets.Any())
            {
                baskets.ForEach(x =>
                {
                    x.Retailer = retailerService.GetById(x.RetailerId);
                    x.Supplier = supplierService.GetById(x.SupplierId);
                });
            }

            return baskets;
        }


        public void AddOrderBasketItem(int retailerId, OrderBasketItem basketItem)
        {
            orderBasketRepository.AddOrderBasketItem(basketItem, databaseConfigAdapter.GetRetailerDatabaseConfigKey(retailerId));
        }


        public List<OrderBasketItem> AllItemsForOrderBasket(int orderBasketId, int retailerId)
        {
            return orderBasketRepository.AllItemsForOrderBasket(orderBasketId, databaseConfigAdapter.GetRetailerDatabaseConfigKey(retailerId));
        }


        public void FinaliseOrder(int orderBasketId, DateTime orderDate, int retailerId)
        {
            orderBasketRepository.FinaliseOrder(orderBasketId, orderDate, databaseConfigAdapter.GetRetailerDatabaseConfigKey(retailerId));
        }

        public OrderBasket CreateNew(int supplierId, int retailerId, int userId)
        {
            return orderBasketRepository.CreateNew(supplierId, retailerId, userId, databaseConfigAdapter.GetRetailerDatabaseConfigKey(retailerId));
        }
    }
}
