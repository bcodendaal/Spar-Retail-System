using SparRetail.Core.Config;
using SparRetail.Core.Database;
using SparRetail.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SparRetail.Products.Repositories
{
    public class ProductRepository : RepositoryBase, IProductRepository
    {
        public ProductRepository(IDatabaseConfigCollection config)
            : base(config)
        {
        }

        public List<Product> GetAllForSupplier(Supplier supplier)
        {
            return QueryList<Product>("usp_SelectProductsForSupplier", new { SupplierId = supplier.SupplierId }, supplier.DatabaseConfigKey);
        }

        public Page<Product> GetSupplierProductsPaged(string databaseConfigKey, ProductPagedParams pageParam)
        {
            return QueryMultiple("usp_SelectSupplierProductsPaged",
                new
                {
                    @SupplierId = pageParam.SupplierId,
                    @SearchText = pageParam.SearchText,
                    @OrderBy = pageParam.OrderBy,
                    @OrderDirection = pageParam.OrderDirection,
                    @PageNumber = pageParam.PageNumber,
                    @PageSize = pageParam.PageSize
                },
                new Func<List<Product>, Page, Page<Product>>(
                    (list, page) => new Page<Product>(page, list)),
                databaseConfigKey);
        }
    }
}
