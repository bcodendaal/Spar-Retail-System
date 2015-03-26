using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SparRetail.Models
{
    public class OpenOrderTotals : OrderBasket
    {
        public int TotalPrice { get; set; }
        public int TotalProducts { get; set; }
    }
}
