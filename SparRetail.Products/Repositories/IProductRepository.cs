using SparRetail.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SparRetail.Products.Repositories
{
    public interface IProductRepository
    {
        List<Product> GetAllForSupplier(Supplier supplier);
        Page<Product> GetSupplierProductsPaged(string databaseConfigKey, Page pageParam);
    }

}
