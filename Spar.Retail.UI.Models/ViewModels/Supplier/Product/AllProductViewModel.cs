using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spar.Retail.UI.Models.ViewModels.Supplier.Product
{
    public class AllProductViewModel
    {
        public SparRetail.Models.Supplier Supplier { get; set; }
        public List<SparRetail.Models.Product> Products { get; set; }
    }
}
