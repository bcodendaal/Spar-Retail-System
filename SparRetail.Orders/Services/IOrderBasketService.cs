using SparRetail.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SparRetail.Orders.Services
{
    public interface IOrderBasketService
    {
        List<OrderBasket> AllForRetailerSupplier(int retailerId, int supplierId);
        List<OrderBasket> AllForRetailer(int retailerId);
        Page<OpenOrderPageResult> AllOpenOrdersForRetailerPaged(OpenOrderPageParams pageParams);
        void AddOrderBasketItem(int retailerId, OrderBasketItem basketItem);
        void UpdateOrderBasketItem(OrderBasketItem basketItem, int retailerId);
        void DeleteOrderBasketItem(OrderBasketItem basketItem, int retailerId);
        List<OrderBasketItem> AllItemsForOrderBasket(int orderBasketId, int retailerId);
        OrderBasket CreateNew(int supplierId, int retailerId, int userId);
        void FinaliseOrder(int orderBasketId, DateTime orderDate, int retailerId);
        OrderBasket Get(int basketId, int retailerId);
        OpenOrderTotals GetOpenOrderTotals(int orderId, int retailerId);
    }
}
