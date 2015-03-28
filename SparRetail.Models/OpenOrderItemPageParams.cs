using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SparRetail.Models
{
    public class OpenOrderItemPageParams: Page
    {
        public int OpenOrderId { get; set; }
        public int RetailerId { get; set; }
    }
}
