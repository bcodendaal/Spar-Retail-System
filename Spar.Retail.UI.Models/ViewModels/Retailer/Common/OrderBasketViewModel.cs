using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SparRetail.Models;

namespace Spar.Retail.UI.Models.ViewModels.Retailer.Common
{
    public class OrderBasketViewModel
    {
        public OrderBasket OrderBasket { get; set; }
        public List<OrderBasketItem> Products { get; set; }
    }
}
