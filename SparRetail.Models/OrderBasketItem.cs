using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SparRetail.Models
{
    public class OrderBasketItem
    {
        public int RetailerOrderBasketItemId { get; set; }
        public int OrderBasketId { get; set; }
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public string ProductCode { get; set; }
        public string BarCode { get; set; }
        public string UnitOfMeasure { get; set; }
        public int NumberOfUnits { get; set; }
        public decimal PricePerUnit {get;set;}
        public decimal TotalPrice { get; set; }
        public DateTime CreatedOn { get; set; }
    }
}
