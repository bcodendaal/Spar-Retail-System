using System;
using System.Collections.Generic;
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

        public ActionResult CreateNewOrder(int SupplierId)
        {
            var model = new ProductsOrderViewModel()
            {
                Order = _orderApi.CreateNew(SupplierId,_profileProvider.GetEntityId(),User.Identity.GetUserId())
            };
            return View("~/Views/Retailer/Orders/ProductsOrder.cshtml");
        }
    }
}
