using SparRetail.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SparRetail.Orders.Repositories
{
    public interface IOrderBasketRepository
    {
        List<OrderBasket> AllForRetailerSupplier(int retailerId, string retailerDbKey, int supplierId);
        List<OrderBasket> AllForRetailer(int retailerId, string retailerDbKey);
        Page<OpenOrderPageResult> AllOpenOrdersForRetailerPaged(OpenOrderPageParams pageParams, string retailerDbKey);
        void AddOrderBasketItem(OrderBasketItem basketItem, string retailerDbKey);
        void UpdateOrderBasketItem(OrderBasketItem basketItem, string retailerDbKey);
        void DeleteOrderBasketItem(OrderBasketItem basketItem, string retailerDbKey);
        List<OrderBasketItem> AllItemsForOrderBasket(int orderBasketId, string retailerDbKey);
        OrderBasket CreateNew(int supplierId, int retailerId, int userId, string retailerDbKey);
        void FinaliseOrder(int orderBasketId, DateTime orderDate, string retailerDbKey);
        OrderBasket Get(int basketId, string retailerDbKey);
        OpenOrderDetails GetOpenOrderTotals(int orderId, string retailerDbKey );

        Page<OrderBasketItem> GetOpenOrderItemsPaged(OpenOrderItemPageParams pageParams, string retailerDbKey);
    }
}
