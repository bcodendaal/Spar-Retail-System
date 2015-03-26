﻿using SparRetail.Models;
using SparRetail.Models.Api;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SparRetail.Interop
{
    public interface IOrderApi
    {
        List<OrderBasket> AllOrderBasketForRetailerSupplier(int retailerId, int supplierId);
        List<OrderBasket> AllOrderBasketForRetailer(int retailerId);
        Page<OpenOrderPageResult> AllOpenOrdersForRetailerPaged(OpenOrderPageParams pageParams);
        ResponseModel AddOrderBasketItem(OrderBasketItemPost basketItemPost);
        ResponseModel UpdateOrderBasketItem(OrderBasketItemPost basketItem);
        ResponseModel DeleteOrderBasketItem(OrderBasketItemPost basketItem);
        List<OrderBasketItem> AllItemsForOrderBasket(int orderBasketItem, int retailerId);
        ResponseModel FinaliseOrder(FinaliseOrderPost finaliseOrderPost);
        OrderBasketResponse CreateNew(int supplierId, int retailerId, int userId);
        List<Order> AllOrderForSupplier(int supplierId);
        OpenOrderTotals GetOpenOrderTotals(int orderId, int retailerId);
        Page<OrderPagedResult> GetAllFinalizedOrdersForRetailer(OrderPageParams pageParams);

    }
}
