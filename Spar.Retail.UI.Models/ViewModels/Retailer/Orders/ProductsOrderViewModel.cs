using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SparRetail.Models;

namespace Spar.Retail.UI.Models.ViewModels.Retailer.Orders
{
    public class ProductsOrderViewModel
    {
        public OpenOrderDetails Order { get; set; }
        public SparRetail.Models.Supplier Supplier { get; set; }
    }
}
