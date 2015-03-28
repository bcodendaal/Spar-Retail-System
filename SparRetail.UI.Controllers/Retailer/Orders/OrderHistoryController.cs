using SparRetail.Interop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using Spar.Retail.UI.Models.ViewModels;
using SparRetail.Models;
using SparRetail.UI.Controllers.Providers;

namespace SparRetail.UI.Controllers.Retailer.Orders
{
    public class OrderHistoryController : Controller
    {
        private ISupplierApi _supplierApi;
        private IProductApi _productApi;
        private IOrderApi _orderApi;
        private IProfileProvider _profileProvider;

        public OrderHistoryController(
            ISupplierApi supplierApi,
            IProductApi productApi,
            IOrderApi orderApi,
            IProfileProvider profileProvider)
        {
            _supplierApi = supplierApi;
            _productApi = productApi;
            _orderApi = orderApi;
            _profileProvider = profileProvider;
        }

        public ActionResult OrderHistory()
        {
            return View(@"~\Views\Retailer\Orders\OrderHistory\OrderHistory.cshtml");
        }

        public JsonResult OrderHistoryDataTableAjax(DataTableParam param)
        {
            var result = _orderApi.GetAllFinalizedOrdersForRetailer(new OrderPageParams()
            {
                RetailerId = _profileProvider.GetEntityId(),
                OrderBy = param.OrderBy,
                OrderDirection = param.OrderDirection,
                PageNumber = param.PageNumber,
                PageSize = param.length,
                SearchText = param.SearchText

            });

            return Json(new DataTableJsonReturnModel<OrderPagedResult>()
            {
                data = result.Results,
                draw = Convert.ToInt32(param.draw),
                error = string.Empty,
                recordsFiltered = result.Results.Count,
                recordsTotal = result.TotalRows
            },
                            JsonRequestBehavior.AllowGet);
        }
    }
}
