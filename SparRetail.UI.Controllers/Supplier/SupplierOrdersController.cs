using SparRetail.Interop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace SparRetail.UI.Controllers.Supplier
{
    public class SupplierOrdersController : Controller
    {
        public ActionResult Index()
        {
            return View(@"~\Views\Supplier\SupplierOrders\Index.cshtml");
        }
    }
}
