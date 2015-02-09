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
        public ActionResult Index()
        {
            return View("~/Views/Retailer/BrowseSuppliers/Index.cshtml");
        }
    }
}
