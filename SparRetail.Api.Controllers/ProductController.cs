using SparRetail.Interop;
using SparRetail.Models;
using SparRetail.Products.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace SparRetail.Api.Controllers
{
    public class ProductController : ApiController, IProductApi
    {
        protected readonly IProductService productService;

        public ProductController(IProductService productService)
        {
            this.productService = productService;
        }

        [HttpPost]
        public List<Product> GetAllForSupplier(Supplier supplier)
        {
            return productService.GetAllForSupplier(supplier);
        }
    }
}
