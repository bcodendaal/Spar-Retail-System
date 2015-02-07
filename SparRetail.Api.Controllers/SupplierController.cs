using SparRetail.Interop;
using SparRetail.Models;
using SparRetail.Suppliers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;


namespace SparRetail.Api.Controllers
{
    public class SupplierController : ApiController, ISupplierApi
    {
        protected readonly ISupplierService supplierService;

        public SupplierController(ISupplierService supplierService)
        {
            this.supplierService = supplierService;
        }

        [HttpGet]
        public string Test()
        {
            return "Test works";
        }

        [HttpGet]
        public List<Supplier> All()
        {
            return supplierService.All();
        }
    }
}
