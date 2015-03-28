using System.Collections.Generic;
using System.Web.Http;
using SparRetail.Interop;
using SparRetail.Models;
using SparRetail.Products.Services;

namespace SparRetail.Api.Controllers
{
    public class ProductController : ApiController, IProductApi
    {
        protected readonly IProductService ProductService;

        public ProductController(IProductService productService)
        {
            ProductService = productService;
        }

        [HttpPost]
        public List<Product> GetAllForSupplier(Supplier supplier)
        {
            return ProductService.GetAllForSupplier(supplier);
        }

        [HttpPost]
        public List<Product> AddProducts(List<Product> products)
        {
            return ProductService.AddProducts(products);
        }
    }
}