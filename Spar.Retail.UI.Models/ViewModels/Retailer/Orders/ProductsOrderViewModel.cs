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
        public Order Order { get; set; }
        public Supplier Supplier { get; set; }
        public List<Product> Products { get; set; }
    }
}
