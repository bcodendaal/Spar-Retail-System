using SparRetail.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SparRetail.Products.Services
{
    public interface IProductService
    {
        List<Product> GetAllForSupplier(Supplier supplier);
    }
}
