using Spar.Retail.UI.Models.ViewModels.Retailer;
using SparRetail.Interop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using SparRetail.Models;


namespace SparRetail.UI.Controllers.Retailer
{
    public class BrowseSuppliersController : Controller
    {
        private ISupplierApi _supplierApi;
        private IProductApi _productApi;

        public BrowseSuppliersController(ISupplierApi supplierApi, IProductApi productApi)
        {
            _supplierApi = supplierApi;
            _productApi = productApi;
        }

        public ActionResult Index()
        {
            var viewmodel = new BrowseSupplierViewModel()
            {
                Suppliers = _supplierApi.All()
            };
            return View("~/Views/Retailer/BrowseSuppliers/Index.cshtml", viewmodel);
        }

        public ActionResult CreateOrder(int supplierId)
        {

            var supplier = _supplierApi.All().FirstOrDefault(x => x.SupplierId == supplierId);
            var viewmodel = new CreateOrderViewModel()
            {
                Supplier = supplier,
                Products = _productApi.GetAllForSupplier(supplier)
            };

            return PartialView(@"~\Views\Retailer\BrowseSuppliers\_CreateOrder.cshtml",viewmodel);
        }
    }
}
