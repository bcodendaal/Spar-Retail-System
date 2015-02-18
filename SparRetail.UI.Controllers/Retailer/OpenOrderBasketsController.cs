using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace SparRetail.UI.Controllers.Retailer
{
    public class OpenOrderBasketsController : Controller
    {

        public ActionResult OpenBaskets()
        {
            return View(@"~\Views\Retailer\RetailerOpenOrder\OpenBaskets.cshtml");
        }
    }
}
