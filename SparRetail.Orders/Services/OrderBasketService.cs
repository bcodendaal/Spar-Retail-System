using MassTransit;
using MassTransit.Log4NetIntegration.Logging;
using SparRetail.Core.Config;
using SparRetail.Core.Logging;
using SparRetail.DatabaseConfigAdapter;
using SparRetail.Models;
using SparRetail.Models.Commands;
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
        protected readonly IConfigCollection configCollection;
        protected readonly IServiceBus bus;

        private const string TagGroup = "OrderBasketService";

        public OrderBasketService(IOrderBasketRepository orderBasketRepository, IDatabaseConfigAdapter databaseConfigAdapter, ISupplierService supplierService, IRetailerService retailerService, IConfigCollection configCollection, ILogger logger)
        {
            this.orderBasketRepository = orderBasketRepository;
            this.databaseConfigAdapter = databaseConfigAdapter;
            this.supplierService = supplierService;
            this.retailerService = retailerService;
            this.configCollection = configCollection;
            this.logger = logger;

            bus = ServiceBusFactory.New(sbc =>
            {
                Log4NetLogger.Use();
                sbc.UseRabbitMq();
                sbc.ReceiveFrom(string.Format("rabbitmq://{0}/{1}", configCollection.Get(SharedConfigKeys.RabbitHost), "orderbasket.finalize.producer"));
            });
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
            bus.Publish<FinalizeOrderCommand>(new FinalizeOrderCommand() { OrderBasketId = orderBasketId, RetailerId = retailerId });
        }

        public OrderBasket CreateNew(int supplierId, int retailerId, int userId)
        {
            return orderBasketRepository.CreateNew(supplierId, retailerId, userId, databaseConfigAdapter.GetRetailerDatabaseConfigKey(retailerId));
        }


        public void UpdateOrderBasketItem(OrderBasketItem basketItem, int retailerId)
        {
            orderBasketRepository.UpdateOrderBasketItem(basketItem,
                databaseConfigAdapter.GetRetailerDatabaseConfigKey(retailerId));
        }

        public void DeleteOrderBasketItem(OrderBasketItem basketItem, int retailerId)
        {
            orderBasketRepository.DeleteOrderBasketItem(basketItem,
               databaseConfigAdapter.GetRetailerDatabaseConfigKey(retailerId));
        }


        public OrderBasket Get(int basketId, int retailerId)
        {
            return orderBasketRepository.Get(basketId, databaseConfigAdapter.GetRetailerDatabaseConfigKey(retailerId));
        }


        public Page<OpenOrderPageResult> AllOpenOrdersForRetailerPaged(OpenOrderPageParams pageParams)
        {
            return orderBasketRepository.AllOpenOrdersForRetailerPaged(
                pageParams,
                databaseConfigAdapter.GetRetailerDatabaseConfigKey(pageParams.RetailerId)
                );
        }

        public OpenOrderDetails GetOpenOrderTotals(int orderId, int retailerId)
        {
            return orderBasketRepository.GetOpenOrderTotals(
                orderId,
                databaseConfigAdapter.GetRetailerDatabaseConfigKey(retailerId)
                );
        }


        public Page<OrderBasketItem> GetOpenOrderItemsPaged(OpenOrderItemPageParams pageParams)
        {
            return orderBasketRepository.GetOpenOrderItemsPaged(pageParams,
                databaseConfigAdapter.GetRetailerDatabaseConfigKey(pageParams.RetailerId)
                );
        }
    }
}
