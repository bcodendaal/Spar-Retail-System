using SparRetail.Core.Config;
using SparRetail.Core.Database;
using SparRetail.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SparRetail.Orders.Repositories
{
    public class OrderRepository : RepositoryBase, IOrderRepository
    {          
        public OrderRepository(IDatabaseConfigCollection config)
            : base(config)
        {

        }

        public List<Order> AllOrderForSupplier(int supplierId, string supplierDbKey)
        {
            return QueryList<Order>("usp_SelectOrdersForSupplier", new { @SupplierId = supplierId }, supplierDbKey);
        }
        public Page<OrderPagedResult> GetAllFinalizedOrdersForRetailer(OrderPageParams pageParams, string retailerDbKey)
        {
            return QueryMultiple("usp_GetAllFinalizedOrdersPaged",
                 new
                 {
                     @RetailerId = pageParams.RetailerId,
                     @SearchText = pageParams.SearchText,
                     @OrderBy = pageParams.OrderBy,
                     @OrderDirection = pageParams.OrderDirection,
                     @PageNumber = pageParams.PageNumber,
                     @PageSize = pageParams.PageSize
                 },
                 new Func<List<OrderPagedResult>, Page, Page<OrderPagedResult>>(
                     (list, page) => new Page<OrderPagedResult>(page, list)),
                 retailerDbKey);
        }
    }
}
