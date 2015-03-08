using SparRetail.Interop;
using SparRetail.Models;
using SparRetail.Products.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using SparRetail.Core.Logging;
using SparRetail.Suppliers;

namespace SparRetail.Api.Controllers
{
    public class ProductController : ApiController, IProductApi
    {
        protected readonly IProductService productService;
        private readonly ISupplierService supplierService;
        protected readonly ILogger logger;
        private const string TagGroup = "ProductController";

        public ProductController(IProductService productService, ILogger logger, ISupplierService supplierService)
        {
            this.productService = productService;
            this.logger = logger;
            this.supplierService = supplierService;
        }

        [HttpPost]
        public List<Product> GetAllForSupplier(Supplier supplier)
        {
            return productService.GetAllForSupplier(supplier);
        }

        [HttpPost]
        public Page<Product> GetSupplierProductsPaged(Page page)
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
    }
}
