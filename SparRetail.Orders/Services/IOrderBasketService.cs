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
        void AddOrderBasketItem(int retailerId, OrderBasketItem basketItem);
    }
}
