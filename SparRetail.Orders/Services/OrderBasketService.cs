using SparRetail.Core.Logging;
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
        protected readonly IRetailerService retailerService;
        protected readonly ISupplierService supplierService;
        protected readonly ILogger logger;
        private const string TagGroup = "OrderBasketService";

        public OrderBasketService(IOrderBasketRepository orderBasketRepository, IRetailerService retailerService, ISupplierService supplierService, ILogger logger)
        {
            this.orderBasketRepository = orderBasketRepository;
            this.retailerService = retailerService;
            this.supplierService = supplierService;
            this.logger = logger;
        }


        public List<OrderBasket> AllForRetailerSupplier(int retailerId, int supplierId)
        {
            var baskets = orderBasketRepository.AllForRetailerSupplier(retailerId, retailerService.GetById(retailerId).DatabaseConfigKey, supplierId);
            
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
            var baskets = orderBasketRepository.AllForRetailer(retailerId, retailerService.GetById(retailerId).DatabaseConfigKey);
            if(baskets != null && baskets.Any())
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
            orderBasketRepository.AddOrderBasketItem(basketItem, retailerService.GetById(retailerId).DatabaseConfigKey);
        }
    }
}
