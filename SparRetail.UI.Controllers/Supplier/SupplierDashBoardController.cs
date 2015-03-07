using SparRetail.Models.Enums;
using SparRetail.UI.Controllers.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace SparRetail.UI.Controllers.Supplier
{
    [TenantTypeFilter(TenantType.Supplier, Order = 10)]
    public class SupplierDashBoardController : Controller
    {
        public ActionResult Index()
        {
            return View("~/Views/Supplier/Dashboard.cshtml");
        }

    }
}
