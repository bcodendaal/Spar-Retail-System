using System.Collections.Generic;
using System.Web.Http;
using SparRetail.Interop;
using SparRetail.Models;
using SparRetail.Products.Services;
using SparRetail.Core.Logging;
using SparRetail.Suppliers;

namespace SparRetail.Api.Controllers
{
    public class ProductController : ApiController, IProductApi
    {
        protected readonly IProductService ProductService;
        private readonly ISupplierService supplierService;
        protected readonly ILogger logger;
        private const string TagGroup = "ProductController";

        public ProductController(IProductService productService, ILogger logger, ISupplierService supplierService)
        {
            ProductService = productService;
            this.logger = logger;
            this.supplierService = supplierService;
        }

        [HttpPost]
        public List<Product> GetAllForSupplier(Supplier supplier)
        {
            return ProductService.GetAllForSupplier(supplier);
        }

        [HttpPost]
        public Page<Product> GetSupplierProductsPaged(ProductPagedParams page)
        {
            try
            {
                return productService.GetSupplierProductsPaged(page);
            }
            catch (Exception e)
            {
                logger.Error(TagGroup, "GetSupplierProductsPaged", e.ToString());
                return new Page<Product>();
            }
        }
   [HttpPost]
        public List<Product> AddProducts(List<Product> products)
        {
            return ProductService.AddProducts(products);
        }
    }
}