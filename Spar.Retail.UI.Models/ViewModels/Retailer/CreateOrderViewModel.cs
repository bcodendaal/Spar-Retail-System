using SparRetail.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spar.Retail.UI.Models.ViewModels.Retailer
{
    public class CreateOrderViewModel
    {
        public Supplier Supplier { get; set; }
        public List<Product> Products { get; set; }
    }
}
