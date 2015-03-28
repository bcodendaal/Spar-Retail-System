using SparRetail.Models;
using SparRetail.Products.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SparRetail.DatabaseConfigAdapter;
using SparRetail.Models.Enums;

namespace SparRetail.Products.Services
{
    public class ProductService : IProductService
    {
        protected readonly IProductRepository productRepository;
        protected readonly IDatabaseConfigAdapter databaseConfigAdapter;

        public ProductService(IProductRepository productRepository, IDatabaseConfigAdapter databaseConfigAdapter)
        {
            this.productRepository = productRepository;
            this.databaseConfigAdapter = databaseConfigAdapter;
        }

        public List<Product> GetAllForSupplier(Supplier supplier)
        {
            return productRepository.GetAllForSupplier(supplier);
        }

        public Page<Product> GetSupplierProductsPaged(ProductPagedParams page)
        {
            return productRepository.GetSupplierProductsPaged(databaseConfigAdapter.GetSupplierDatabaseConfigKey(page.SupplierId), page);
        }
    }
}
