using SparRetail.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SparRetail.Interop
{
    public interface IProductApi
    {
        List<Product> GetAllForSupplier(Supplier supplier);
        Page<Product> GetSupplierProductsPaged(Page page);
    }
}
