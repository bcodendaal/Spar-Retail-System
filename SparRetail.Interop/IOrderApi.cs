using SparRetail.Models;
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
        ResponseModel AddOrderBasketItem(OrderBasketItemPost basketItemPost);
    }
}
