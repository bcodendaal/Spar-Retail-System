using SparRetail.Interop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace SparRetail.UI.Controllers.Retailer
{
    public class BrowseSuppliersController : Controller
    {
        private ISupplierApi _supplierApi;

        public BrowseSuppliersController(ISupplierApi supplierApi)
        {
            _supplierApi = supplierApi;
        }

        public ActionResult Index()
        {
            var suppliers = _supplierApi.All();
            return View("~/Views/Retailer/BrowseSuppliers/Index.cshtml");
        }
    }
}
