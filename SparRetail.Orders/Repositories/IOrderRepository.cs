using SparRetail.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SparRetail.Orders.Repositories
{
    public interface IOrderRepository
    {
        List<Order> AllOrderForSupplier(int supplierId, string supplierDbKey);
        Page<OrderPagedResult> GetAllFinalizedOrdersForRetailer(OrderPageParams pageParams, string retailerDbKey);
    }
}
