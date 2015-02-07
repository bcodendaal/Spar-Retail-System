using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SparRetail.Models
{
    public class Product
    {
        public int ProductId { get; set; }
        public int SupplierId { get; set; }
        public string ProductCode { get; set; }
        public string ProductName { get; set; }
        public string ProductDescription { get; set; }
        public decimal VAT { get; set; }
        public string Barcode { get; set; }
        public int CategoryId { get; set; }
        public int UnitOfMeasureProductId { get; set; }
        public int UnitOfMeasureId { get; set; }
        public string UnitOfMeasureName { get; set; }
        public decimal Price { get; set; }
        public int PackSize { get; set; }
        public string CreatedOn { get; set; }
    }
}
