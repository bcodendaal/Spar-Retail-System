﻿using SparRetail.Interop;
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
            return Get<List<OrderBasket>>("AllOrderBasketForRetailerSupplier", new Dictionary<string, string> { { "retailerId", retailerId.ToString()},  { "supplierId", supplierId.ToString() } });
        }

        public List<OrderBasket> AllOrderBasketForRetailer(int retailerId)
        {
            return Get<List<OrderBasket>>("AllOrderBasketForRetailer", new Dictionary<string, string> { { "retailerId", retailerId.ToString() }});
        }


        public ResponseModel AddOrderBasketItem(OrderBasketItemPost basketItemPost)
        {
            return Post<ResponseModel>("AddOrderBasketItem", basketItemPost);
        }
    }
}