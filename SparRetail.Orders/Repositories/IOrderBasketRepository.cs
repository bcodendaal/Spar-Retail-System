﻿using SparRetail.Models;
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
        void AddOrderBasketItem(OrderBasketItem basketItem, string retailerDbKey);
    }
}