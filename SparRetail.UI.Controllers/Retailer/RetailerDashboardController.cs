using SparRetail.Models.Enums;
using SparRetail.UI.Controllers.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace SparRetail.UI.Controllers.Retailer
{
    [TenantTypeFilter(TenantType.Retailer, Order = 10)]
    public class RetailerDashboardController : Controller
    {
        [Authorize]
        public ActionResult Index()
        {
            return View("~/Views/Retailer/Dashboard.cshtml");
        }
    }
}
