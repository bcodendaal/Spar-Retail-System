using SparRetail.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SparRetail.Models
{
    public class Order
    {
        public int OrderId { get; set; }
        public int RetailerId { get; set; }
        public Retailer Retailer { get; set; }
        public int SupplierId { get; set; }
        public Supplier Supplier { get; set; }
        public int UserId { get; set; }
        public OrderStatus OrderStatus { get; set; }
        public DateTime CreatedOn { get; set; }
    }
}
