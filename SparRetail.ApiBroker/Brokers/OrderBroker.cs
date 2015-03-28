using SparRetail.Interop;
using SparRetail.Models;
using SparRetail.Models.Api;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SparRetail.ApiBroker.Brokers
{
    public class OrderBroker : ApiBrokerBase, IOrderApi
    {
        public OrderBroker(IApiBrokerConfig config)
            : base(config, "order")
        {
        }

        public List<OrderBasket> AllOrderBasketForRetailerSupplier(int retailerId, int supplierId)
        {
            return Get<List<OrderBasket>>("AllOrderBasketForRetailerSupplier", new Dictionary<string, string> { { "retailerId", retailerId.ToString() }, { "supplierId", supplierId.ToString() } });
        }

        public List<OrderBasket> AllOrderBasketForRetailer(int retailerId)
        {
            return Get<List<OrderBasket>>("AllOrderBasketForRetailer", new Dictionary<string, string> { { "retailerId", retailerId.ToString() } });
        }


        public ResponseModel AddOrderBasketItem(OrderBasketItemPost basketItemPost)
        {
            return Post<ResponseModel>("AddOrderBasketItem", basketItemPost);
        }


        public List<OrderBasketItem> AllItemsForOrderBasket(int orderBasketId, int retailerId)
        {
            return Get<List<OrderBasketItem>>("AllItemsForOrderBasket", new Dictionary<string, string> { { "orderbasketId", orderBasketId.ToString() }, { "retailerId", retailerId.ToString() } });
        }


        public ResponseModel FinaliseOrder(FinaliseOrderPost finaliseOrderPost)
        {
            return Post<ResponseModel>("FinaliseOrder", new FinaliseOrderPost { OrderBasketId = finaliseOrderPost.OrderBasketId, RetailerId = finaliseOrderPost.RetailerId });
        }


        public OrderBasketResponse CreateNew(int supplierId, int retailerId, int userId)
        {
            return Get<OrderBasketResponse>("CreateNew", new Dictionary<string, string> { { "supplierId", supplierId.ToString() }, { "retailerId", retailerId.ToString() }, { "userId", userId.ToString() } });
        }


        public ResponseModel UpdateOrderBasketItem(OrderBasketItemPost basketItem)
        {
            return Post<ResponseModel>("UpdateOrderBasketItem", basketItem);
        }

        public ResponseModel DeleteOrderBasketItem(OrderBasketItemPost basketItem)
        {
            return Post<ResponseModel>("DeleteOrderBasketItem", basketItem);
        }


        public List<Order> AllOrderForSupplier(int supplierId)
        {
            return Get<List<Order>>("AllOrderForSupplier", new Dictionary<string, string> { { "supplierId", supplierId.ToString() } });
        }


        public Page<OpenOrderPageResult> AllOpenOrdersForRetailerPaged(OpenOrderPageParams pageParams)
        {
            return Post<Page<OpenOrderPageResult>>("AllOpenOrdersForRetailerPaged", pageParams);
        }

        public OpenOrderDetails GetOpenOrderDetail(int orderId, int retailerId)
        {
            return Get<OpenOrderDetails>("GetOpenOrderDetail", new Dictionary<string, string> { { "orderId", orderId.ToString() }, { "retailerId", retailerId.ToString() } });
        }


        public Page<OrderPagedResult> GetAllFinalizedOrdersForRetailer(OrderPageParams pageParams)
        {
            return Post<Page<OrderPagedResult>>("GetAllFinalizedOrdersForRetailer", pageParams);
        }


        public Page<OrderBasketItem> GetOpenOrderItemsPaged(OpenOrderItemPageParams pageParams)
        {
            return Post<Page<OrderBasketItem>>("GetOpenOrderItemsPaged", pageParams);
        }
    }
}
