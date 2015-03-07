using SparRetail.Core.Logging;
using SparRetail.Interop;
using SparRetail.Models;
using SparRetail.Models.Api;
using SparRetail.Orders.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace SparRetail.Api.Controllers
{
    public class OrderController : ApiController, IOrderApi
    {
        protected readonly IOrderBasketService orderBasketService;
        protected readonly IOrderService orderService;
        protected readonly ILogger logger;

        private const string TagGroup = "OrderController";

        public OrderController(IOrderBasketService orderBasketService, IOrderService orderService, ILogger logger)
        {
            this.orderBasketService = orderBasketService;
            this.orderService = orderService;
            this.logger = logger;
        }

        [HttpGet]
        public List<OrderBasket> AllOrderBasketForRetailerSupplier(int retailerId, int supplierId)
        {
            return orderBasketService.AllForRetailerSupplier(retailerId, supplierId);
        }

        [HttpGet]
        public List<OrderBasket> AllOrderBasketForRetailer(int retailerId)
        {
            return orderBasketService.AllForRetailer(retailerId);
        }

        [HttpPost]
        public ResponseModel AddOrderBasketItem(OrderBasketItemPost basketItemPost)
        {
            try
            {
                orderBasketService.AddOrderBasketItem(basketItemPost.RetailerId, basketItemPost.OrderBasketItem);
                return new ResponseModel { IsCallSuccess = true, IsCommandSuccess = true, Message = "Success" };
            }
            catch (Exception ex)
            {
                logger.Error(TagGroup, "AddOrderBasketItem", ex);
                return new ResponseModel { IsCallSuccess = true, IsCommandSuccess = false, Message = ex.ToString() };
            }
        }

        [HttpGet]
        public List<OrderBasketItem> AllItemsForOrderBasket(int orderBasketId, int retailerId)
        {
            return orderBasketService.AllItemsForOrderBasket(orderBasketId, retailerId);
        }

        [HttpPost]
        public ResponseModel FinaliseOrder(FinaliseOrderPost finaliseOrderPost)
        {
            try
            {
                orderBasketService.FinaliseOrder(finaliseOrderPost.OrderBasketId, DateTime.Now, finaliseOrderPost.RetailerId);
                return new ResponseModel { IsCallSuccess = true, IsCommandSuccess = true, Message = "Success" };

            }
            catch (Exception ex)
            {
                logger.Error(TagGroup, "FinaliseOrder", ex);
                return new ResponseModel { IsCallSuccess = true, IsCommandSuccess = false, Message = ex.ToString() };
            }

        }

        [HttpGet]
        public OrderBasketResponse CreateNew(int supplierId, int retailerId, int userId)
        {
            try
            {
                return new OrderBasketResponse()
                {
                    OrderBasket = orderBasketService.CreateNew(supplierId, retailerId, 1),
                    IsCallSuccess = true,
                    IsCommandSuccess = true,
                    Message = "Created new Order Basket"
                };

            }
            catch (Exception ex)
            {
                logger.Error(TagGroup, "CreateNew", ex);
                return new OrderBasketResponse { IsCallSuccess = true, IsCommandSuccess = false, Message = ex.ToString(), OrderBasket = new OrderBasket() };
            }
        }

        [HttpPost]
        public ResponseModel UpdateOrderBasketItem(OrderBasketItemPost basketItem)
        {
            try
            {
                orderBasketService.UpdateOrderBasketItem(basketItem.OrderBasketItem, basketItem.RetailerId);
                return new ResponseModel { IsCallSuccess = true, IsCommandSuccess = true, Message = "Success" };
            }
            catch (Exception ex)
            {
                logger.Error(TagGroup, "AddOrderBasketItem", ex);
                return new ResponseModel { IsCallSuccess = true, IsCommandSuccess = false, Message = ex.ToString() };
            }
        }

        [HttpPost]
        public ResponseModel DeleteOrderBasketItem(OrderBasketItemPost basketItem)
        {
            try
            {
                orderBasketService.DeleteOrderBasketItem(basketItem.OrderBasketItem, basketItem.RetailerId);
                return new ResponseModel { IsCallSuccess = true, IsCommandSuccess = true, Message = "Success" };
            }
            catch (Exception ex)
            {
                logger.Error(TagGroup, "AddOrderBasketItem", ex);
                return new ResponseModel { IsCallSuccess = true, IsCommandSuccess = false, Message = ex.ToString() };
            }
        }


        public List<Order> AllOrderForSupplier(int supplierId)
        {
            try
            {
                return orderService.AllOrderForSupplier(supplierId);
            }
            catch (Exception ex)
            {
                logger.Error(TagGroup, "AllOrderForSupplier", ex);
                return new List<Order>();
            }
        }
    }
}
