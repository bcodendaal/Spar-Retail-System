using SparRetail.Models;
using SparRetail.Products.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using log4net;
using SparRetail.DatabaseConfigAdapter;

namespace SparRetail.Products.Services
{
    public class ProductService : IProductService
    {
        protected readonly IProductRepository ProductRepository;
        protected readonly IDatabaseConfigAdapter DatabaseConfigAdapter;
        protected readonly ILog Logger;


        public ProductService(IProductRepository productRepository, IDatabaseConfigAdapter databaseConfigAdapter)
        {
            ProductRepository = productRepository;
            DatabaseConfigAdapter = databaseConfigAdapter;
            Logger = LogManager.GetLogger(GetType());
        }

        public List<Product> GetAllForSupplier(Supplier supplier)
        {
            
            string supplierDatabaseConfigKey = DatabaseConfigAdapter.GetSupplierDatabaseConfigKey(supplier.SupplierId);
            return ProductRepository.GetAllForSupplier(supplier, supplierDatabaseConfigKey);
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
