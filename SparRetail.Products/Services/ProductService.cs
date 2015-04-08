using SparRetail.Models;
using SparRetail.Products.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SparRetail.DatabaseConfigAdapter;
using SparRetail.Models.Enums;
using log4net;
using SparRetail.DatabaseConfigAdapter;

namespace SparRetail.Products.Services
{
    public class ProductService : IProductService
    {
        protected readonly IProductRepository ProductRepository;
        protected readonly IDatabaseConfigAdapter DatabaseConfigAdapter;
        protected readonly ILog Logger;
        protected readonly IDatabaseConfigAdapter databaseConfigAdapter;

        
        
        public ProductService(IProductRepository productRepository, IDatabaseConfigAdapter databaseConfigAdapter)
        {
            ProductRepository = productRepository;
            DatabaseConfigAdapter = databaseConfigAdapter;
            Logger = LogManager.GetLogger(GetType());
            this.databaseConfigAdapter = databaseConfigAdapter;
        }

        public List<Product> GetAllForSupplier(Supplier supplier)
        {
            
            string supplierDatabaseConfigKey = DatabaseConfigAdapter.GetSupplierDatabaseConfigKey(supplier.SupplierId);
            return ProductRepository.GetAllForSupplier(supplier, supplierDatabaseConfigKey);
        }

        public Page<Product> GetSupplierProductsPaged(ProductPagedParams page)
        {
            return ProductRepository.GetSupplierProductsPaged(databaseConfigAdapter.GetSupplierDatabaseConfigKey(page.SupplierId), page);
        }
        
        public List<Product> AddProducts(List<Product> products)
        {
            var resultsList = new List<Product>();

            foreach (var product in products)
            {
                var databaseKey = DatabaseConfigAdapter.GetSupplierDatabaseConfigKey(product.SupplierId);
                resultsList.Add(ProductRepository.AddProducts(product, databaseKey));
            }

            return resultsList;
        }
    }
}
