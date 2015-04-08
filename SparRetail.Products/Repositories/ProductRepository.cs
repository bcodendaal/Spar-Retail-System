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

        public List<Product> GetAllForSupplier(Supplier supplier, string databaseConfigKey)
        {
            return QueryList<Product>("usp_SelectProductsForSupplier", new { SupplierId = supplier.SupplierId }, databaseConfigKey);
        }

        public Product AddProducts(Product product, string databaseConfigKey)
        {
            return QueryOne<Product>("usp_InsertProduct", 
                new {
                    @SupplierId = product.SupplierId,
                    @ProductCode = product.ProductCode,
                    @ProductName = product.ProductName,
                    @ProductDescription = product.ProductDescription,
                    @VAT  = product.VAT,
                    @Barcode = product.Barcode,
                    @CategoryId = product.CategoryId
            }, databaseConfigKey);
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
