using Spar.Retail.UI.Models.ViewModels.SupplierCommon;
using SparRetail.Interop;
using SparRetail.UI.Controllers.Providers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace SparRetail.UI.Controllers.Supplier
{
    public class SupplierCommonController : Controller
    {
        protected IOrderApi _orderApi;
        protected IProfileProvider _profileProvider;

        public SupplierCommonController(IOrderApi orderApi, IProfileProvider profileProvider)
        {
            _orderApi = orderApi;
            _profileProvider = profileProvider;
        }

        public ActionResult OrderHistory()
        {
            var orders = _orderApi.AllOrderForSupplier(_profileProvider.GetEntityId());


            return View(@"~\Views\Supplier\SupplierCommon\OrderHistory.cshtml", new OrderHistoryViewModel() { Orders = orders });

        }
       
    }
}
