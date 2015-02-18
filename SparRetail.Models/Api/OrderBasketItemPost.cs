using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SparRetail.Models.Api
{
    public class OrderBasketItemPost : RetailerIdPost
    {
        public OrderBasketItem OrderBasketItem { get; set; }
    }
}
