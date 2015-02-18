using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SparRetail.Models.Api;

namespace SparRetail.Models.Api
{
   public class OrderBasketResponse :ResponseModel
    {
       public OrderBasket OrderBasket { get; set; }
    }
}
