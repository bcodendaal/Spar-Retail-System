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
    public class ProductRepository : RepositoryBase,  IProductRepository
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
    }
}
