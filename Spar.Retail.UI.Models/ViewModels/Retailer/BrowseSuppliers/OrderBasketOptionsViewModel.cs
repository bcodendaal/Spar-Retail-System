using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SparRetail.Models;

namespace Spar.Retail.UI.Models.ViewModels.Retailer.BrowseSuppliers
{
    public class OrderBasketOptionsViewModel
    {
        public int SupplierId { get; set; }
        public int RetailerId { get; set; }
        public List<OrderBasket> OrderBaskets { get; set; }
    }
}
