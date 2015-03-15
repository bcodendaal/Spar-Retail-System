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
    public class OrderBasketRepository : RepositoryBase, IOrderBasketRepository
    {
        public OrderBasketRepository(IDatabaseConfigCollection config)
            : base(config)
        {

        }

        public List<OrderBasket> AllForRetailerSupplier(int retailerId, string retailerDBKey, int supplierId)
        {
            return QueryList<OrderBasket>("usp_SelectOrderBasketForRetailerSupplier", new { @RetailerId = retailerId, @SupplierId = supplierId }, retailerDBKey);
        }

        public List<OrderBasket> AllForRetailer(int retailerId, string retailerDBKey)
        {
            return QueryList<OrderBasket>("usp_SelectOrderBasketForRetailerSupplier", new { @RetailerId = retailerId, @SupplierId = 0 }, retailerDBKey);
        }


        public void AddOrderBasketItem(OrderBasketItem basketItem, string retailerDbKey)
        {
            Execute("usp_InsertOrderBasketItem", new
            {
                @OrderBasketId = basketItem.OrderBasketId,
                @ProductId = basketItem.ProductId,
                @ProductName = basketItem.ProductName,
                @ProductCode = basketItem.ProductCode,
                @BarCode = basketItem.BarCode,
                @UnitOfMeasure = basketItem.UnitOfMeasure,
                @NumberOfUnits = basketItem.NumberOfUnits,
                @PricePerUnit = basketItem.PricePerUnit,
                @TotalPrice = basketItem.TotalPrice
            }, retailerDbKey);
        }

        public List<OrderBasketItem> AllItemsForOrderBasket(int orderBasketId, string retailerDbKey)
        {
            return QueryList<OrderBasketItem>("usp_SelectOrderBasketItemsForOrder", new { @OrderBasketId = orderBasketId }, retailerDbKey);
        }


        public void FinaliseOrder(int orderBasketId, DateTime orderDate, string retailerDbKey)
        {
            Execute("usp_FinaliseOrder", new { @OrderBasketId = orderBasketId, @OrderDate = orderDate }, retailerDbKey);
        }


        public OrderBasket CreateNew(int supplierId, int retailerId, int userId, string retailerDbKey)
        {
            return QueryOne<OrderBasket>("usp_CreateRetailerOrderBasket", new { @supplierId = supplierId, @retailerId = retailerId, @userId = userId }, retailerDbKey);
        }


        public void UpdateOrderBasketItem(OrderBasketItem basketItem, string retailerDbKey)
        {
            Execute("usp_UpdateOrderItem", new
            {
                @OrderBasketId = basketItem.OrderBasketId,
                @OrderBasketItemId = basketItem.RetailerOrderBasketItemId,
                @NumberOfUnits = basketItem.NumberOfUnits,
                @TotalPrice = basketItem.TotalPrice
            }, retailerDbKey);
        }

        public void DeleteOrderBasketItem(OrderBasketItem basketItem, string retailerDbKey)
        {
            Execute("usp_DelteOrderBasketItem", new
            {
                @OrderBasketId = basketItem.OrderBasketId,
                @OrderBasketItemId = basketItem.RetailerOrderBasketItemId
            }, retailerDbKey);
        }


        public List<Order> AllOrderForSupplier(int supplierId, string supplierDbKey)
        {
            throw new NotImplementedException();
        }


        public OrderBasket Get(int basketId, string retailerDbKey)
        {
            return QueryOne<OrderBasket>("usp_SelectOrderBasket", new { @BasketId = basketId }, retailerDbKey);
        }
    }
}
