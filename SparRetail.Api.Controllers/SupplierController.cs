using SparRetail.Interop;
using SparRetail.Models;
using SparRetail.Models.Api;
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

        [HttpPost]
        public CreateSupplierResponse Create(Supplier supplier)
        {
            try
            {
                var result = supplierService.Create(supplier);
                return new CreateSupplierResponse { IsCommandSuccess = result.IsSuccess, Message = result.Message, IsCallSuccess = true, SupplierId = result.Model.SupplierId };
            }
            catch (Exception ex)
            {
                return new CreateSupplierResponse { IsCallSuccess = true, Message = ex.ToString(), IsCommandSuccess = false };
            }

        }
        [HttpPost]
        public Page<Supplier> GetAllSuppliersForRetailerPaged(SupplierPagedParams pageParam)
        {
            try
            {
                return supplierService.GetAllSuppliersForRetailerPaged(pageParam);
            }
            catch (Exception)
            {
                return new Page<Supplier>();
            }
        }
    }
}
