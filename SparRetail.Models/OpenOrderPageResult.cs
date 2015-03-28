using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SparRetail.Models
{
    public class OpenOrderPageResult : OrderBasket
    {
        public string SupplierName { get; set; }
        public string SupplierCode { get; set; }
    }
}
