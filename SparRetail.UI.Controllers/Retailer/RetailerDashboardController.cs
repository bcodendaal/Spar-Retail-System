using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace SparRetail.UI.Controllers.Retailer
{
    public class RetailerDashboardController : Controller
    {

        public ActionResult Index()
        {
            return View("~/Views/Retailer/Dashboard.cshtml");
        }
    }
}
