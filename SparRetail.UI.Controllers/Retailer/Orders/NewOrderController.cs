using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Spar.Retail.UI.Models.ViewModels;
using Spar.Retail.UI.Models.ViewModels.Retailer.Orders;
using SparRetail.Interop;
using SparRetail.Models;
using SparRetail.Models.Api;
using SparRetail.UI.Controllers.Providers;

namespace SparRetail.UI.Controllers.Retailer.Orders
{
    public class NewOrderController : Controller
    {
        private ISupplierApi _supplierApi;
        private IProductApi _productApi;
        private IOrderApi _orderApi;
        private IProfileProvider _profileProvider;

        public NewOrderController(ISupplierApi supplierApi, IProductApi productApi, IOrderApi orderApi,
            IProfileProvider profileProvider)
        {
            _supplierApi = supplierApi;
            _productApi = productApi;
            _orderApi = orderApi;
            _profileProvider = profileProvider;
        }

        public ActionResult SelectSupplier()
        {
            return View(@"~\Views\Retailer\Orders\NewOrder\SelectSupplier.cshtml");
        }

        public ActionResult SuppliersDataTableAjax(DataTableParam param)
        {
            var result = _supplierApi.GetAllSuppliersForRetailerPaged(new SupplierPagedParams()
            {
                RetailerId = _profileProvider.GetEntityId(),
                OrderBy = param.OrderBy,
                OrderDirection = param.OrderDirection,
                PageNumber = param.PageNumber,
                PageSize = param.length,
                SearchText = param.SearchText

            });

            return Json(new DataTableJsonReturnModel<Models.Supplier>()
            {
                data = result.Results,
                draw = Convert.ToInt32(param.draw),
                error = string.Empty,
                recordsFiltered = result.Results.Count,
                recordsTotal = result.TotalRows
            },
            JsonRequestBehavior.AllowGet);
        }

        public ActionResult CreateNewOrder(int supplierId)
        {

            return RedirectToAction("AddProducts", "OpenOrder",
                new
                {
                    orderId = _orderApi.CreateNew(supplierId, _profileProvider.GetEntityId(), 1).OrderBasket.OrderBasketId
                });
        }

        /// <summary>
        /// Checks to see if the retailer already created Open Orders for this supplier
        /// If No Open Orders are found, the Retailer Is redirected to the "AddProducts" screen
        /// </summary>
        public ActionResult CheckOpenOrdersForSupplier(int SupplierId)
        {
            var openOrders = _orderApi.AllOrderBasketForRetailerSupplier(_profileProvider.GetEntityId(),SupplierId);
            if (openOrders.Any())
            {
                return Json(new {Success = true, OrderCount = openOrders.Count()}, JsonRequestBehavior.AllowGet);
            }
            //If No Open Orders for Supplier the User Is redirected
            return Json(new { Success = true, OrderCount = 0}, JsonRequestBehavior.AllowGet);
        }
    }
}
