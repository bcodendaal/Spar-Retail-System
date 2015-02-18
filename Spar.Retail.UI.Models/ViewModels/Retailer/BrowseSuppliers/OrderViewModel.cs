using SparRetail.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spar.Retail.UI.Models.ViewModels.Retailer.BrowseSuppliers
{
    public class OrderViewModel
    {
        public List<OrderBasketItem> OrderBasketItems { get; set; }
        public double BasketTotal { get; set; }
    }
}
