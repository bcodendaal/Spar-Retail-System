using SparRetail.DatabaseConfigAdapter;
using SparRetail.Models;
using SparRetail.Orders.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SparRetail.Orders.Services
{
    public class OrderService : IOrderService
    {
        protected IOrderRepository orderRepository;
        protected IDatabaseConfigAdapter databaseConfigAdapter;

        public OrderService(IOrderRepository orderRepository, IDatabaseConfigAdapter databaseConfigAdapter)
        {
            this.orderRepository = orderRepository;
            this.databaseConfigAdapter = databaseConfigAdapter;
        }

        public List<Order> AllOrderForSupplier(int supplierId)
        {
            return orderRepository.AllOrderForSupplier(supplierId, databaseConfigAdapter.GetSupplierDatabaseConfigKey(supplierId));
        }
    }
}
