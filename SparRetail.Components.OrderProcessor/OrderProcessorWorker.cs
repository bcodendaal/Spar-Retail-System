using SparRetail.Core.Email;
using SparRetail.Core.Logging;
using SparRetail.DatabaseConfigAdapter;
using SparRetail.Models.Api;
using SparRetail.Models.Commands;
using SparRetail.Orders.Services;
using SparRetail.Retailers.Services;
using SparRetail.Suppliers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using log4net;
using Newtonsoft.Json;

namespace SparRetail.Components.OrderProcessor
{
    public class OrderProcessorWorker : IOrderProcessorWorker
    {
        protected readonly IOrderService orderService;
        protected readonly IOrderBasketService basketService;
        protected readonly ISupplierService supplierService;
        protected readonly IRetailerService retailerService;        
        protected readonly IOrderProcessorWorkerRepository processorWorkerRepository;
        protected readonly IDatabaseConfigAdapter databaseConfigAdapter;
        protected readonly IMailer mailer;
        protected readonly ILog logger;

        private const string TagGroup = "OrderProcessWorker";

        public OrderProcessorWorker(
            IOrderService orderService, 
            IOrderBasketService basketService, 
            IRetailerService retailerService,
            ISupplierService supplierService,
            IOrderProcessorWorkerRepository processorWorkerRepository, 
            IDatabaseConfigAdapter databaseConfigAdapter,
            IMailer mailer
            )
        {
            this.orderService = orderService;
            this.basketService = basketService;
            this.supplierService = supplierService;
            this.retailerService = retailerService;
            this.processorWorkerRepository = processorWorkerRepository;
            this.databaseConfigAdapter = databaseConfigAdapter;
            this.mailer = mailer;
            this.logger = LogManager.GetLogger(GetType());
        }

        public ResponseModel FinalizeOrder(FinalizeOrderCommand command)
        {
            /* 
             * Get order basket
             * Get order basket items
             * 
             * Insert supplier order
             * Insert supplier items
             * Insert retailer order
             * Insert retailer items
             * Delete basket items
             * Delete basket
             *              
             * Send email to supplier
             */
            logger.Info(string.Format("Received message."));
            logger.Info(string.Format(JsonConvert.SerializeObject(command)));
            try
            {
                // Get the basket and its items
                var basket = basketService.Get(command.OrderBasketId, command.RetailerId);
                var items = basketService.AllItemsForOrderBasket(command.OrderBasketId, command.RetailerId);

                // Insert the orders and order items with transactions
                var retailerConfigKey = databaseConfigAdapter.GetRetailerDatabaseConfigKey(basket.RetailerId);
                var supplierConfigKey = databaseConfigAdapter.GetSupplierDatabaseConfigKey(basket.SupplierId);

                var orderIds = processorWorkerRepository.InsertOrders(basket, items, retailerConfigKey, supplierConfigKey);
   
                // Send email
                mailer.SendMail("You have succesfully placed an order!");

                logger.Info("Operation successful");
                return new ResponseModel { IsCallSuccess = true, IsCommandSuccess = true, Message = "Success" };

            }
            catch (Exception ex)
            {                
                logger.Error(ex);
                return new ResponseModel { IsCallSuccess = true, IsCommandSuccess = false, Message = ex.ToString() };
            }

        }
    }
}
