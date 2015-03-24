using SparRetail.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SparRetail.Components.OrderProcessor
{
    public interface IOrderProcessorWorkerRepository
    {
        /// <summary>
        /// Inserts the open order into the retailer's order / order items tables. 
        /// Inserts the open order into the supplier's order / order items tables
        /// Deletes the open order from the retailers tables        
        /// </summary>
        /// <param name="basket"></param>
        /// <param name="items"></param>
        /// <param name="retailerConfigKey"></param>
        /// <param name="supplierConfigKey"></param>
        /// <returns>An array of ints containing the retailer's order id [0] and the supplier's order id [1]</returns>
        int[] InsertOrders(OrderBasket basket, List<OrderBasketItem> items, string retailerConfigKey, string supplierConfigKey);
    }
}
