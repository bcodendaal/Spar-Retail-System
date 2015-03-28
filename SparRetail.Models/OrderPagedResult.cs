using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SparRetail.Models
{
    public class OrderPagedResult : Order
    {
       public string SupplierName { get; set; }
       public string SupplierCode { get; set; }
       public int TotalProducts { get; set; }
       public int TotalPrice { get; set; }
       public int SupplierId { get; set; }
    }
}
