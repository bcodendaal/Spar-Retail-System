using SparRetail.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SparRetail.Orders.Services
{
    public interface IOrderService
    {
        List<Order> AllOrderForSupplier(int supplierId);
        Page<OrderPagedResult> GetAllFinalizedOrdersForRetailer(OrderPageParams pageParams);
    }
}
