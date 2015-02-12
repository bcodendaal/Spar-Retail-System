using Spar.Retail.UI.Models.ViewModels.Retailer;
using SparRetail.Interop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace SparRetail.UI.Controllers.Retailer
{
    public class RetailerOrdersController : Controller
    {
        private ISupplierApi _supplierApi;

        public RetailerOrdersController(ISupplierApi supplierApi)
        {
            _supplierApi = supplierApi;
        }

        public ActionResult Index()
        {
            return View(@"~\Views\Retailer\RetailerOrders\Index.cshtml");
        }

    }
}
