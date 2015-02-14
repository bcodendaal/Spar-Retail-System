using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spar.Retail.UI.Models.ViewModels.Retailer.BrowseSuppliers
{
    public class AddProductsToBasketViewModel
    {
        public int SupplierId { get; set; }
        public int BasketId { get; set; }
        public int RetailerId { get; set; }
    }
}
