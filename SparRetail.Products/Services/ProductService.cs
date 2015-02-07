using SparRetail.Models;
using SparRetail.Products.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SparRetail.Products.Services
{
    public class ProductService : IProductService
    {
        protected readonly IProductRepository productRepository;

        public ProductService(IProductRepository productRepository)
        {
            this.productRepository = productRepository;
        }

        public List<Product> GetAllForSupplier(Supplier supplier)
        {
            return productRepository.GetAllForSupplier(supplier);
        }
    }
}
