using Spar.Retail.UI.Models.ViewModels.Retailer;
using SparRetail.Interop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using Spar.Retail.UI.Models.ViewModels.Retailer.BrowseSuppliers;
using Spar.Retail.UI.Models.ViewModels.Retailer.Common;
using SparRetail.Models;
using SparRetail.Models.Api;
using SparRetail.UI.Controllers.Providers;
using SparRetail.UI.Controllers.Filters;
using SparRetail.Models.Enums;


namespace SparRetail.UI.Controllers.Retailer
{
    [TenantTypeFilter(TenantType.Retailer, Order = 10)]
    public class BrowseSuppliersController : Controller
    {
        private ISupplierApi _supplierApi;
        private IProductApi _productApi;
        private IOrderApi _orderApi;
        private IProfileProvider _profileProvider;

        public BrowseSuppliersController(ISupplierApi supplierApi, IProductApi productApi, IOrderApi orderApi, IProfileProvider profileProvider)
        {
            _supplierApi = supplierApi;
            _productApi = productApi;
            _orderApi = orderApi;
            _profileProvider = profileProvider;
        }

        public ActionResult Index()
        {
            return View("~/Views/Retailer/BrowseSuppliers/Index.cshtml");
        }

        public ActionResult LoadOrderBasketOptions(int supplierId)
        {

            //check if any orders exists
            var orderbaskets = _orderApi.AllOrderBasketForRetailerSupplier(_profileProvider.GetEntityId(), supplierId);
            if (orderbaskets.Any())
            {
                //if any orders exists user must choose one or choose to create a new order
                var viewmodel = new OrderBasketOptionsViewModel()
                {
                    OrderBaskets = orderbaskets,
                    RetailerId = _profileProvider.GetEntityId(),
                    SupplierId = supplierId
                };
                return PartialView("~/Views/Retailer/BrowseSuppliers/_OrderBasketOption.cshtml", viewmodel);
            }
            //else create a new order and load products
            else
            {
                return CreateNewBasket(supplierId);
            }
        }

        public ActionResult CreateNewBasket(int supplierId)
        {
            var response = _orderApi.CreateNew(supplierId, _profileProvider.GetEntityId(), 1);
            var viewmodel = new AddProductsToBasketViewModel()
            {
                BasketId = response.OrderBasket.OrderBasketId,
                SupplierId = supplierId,
                RetailerId = _profileProvider.GetEntityId()
            };
            return PartialView("~/Views/Retailer/BrowseSuppliers/_AddProductsToBasket.cshtml", viewmodel);
        }

        public ActionResult SelectBasket(int basketId, int supplierId)
        {

            var viewmodel = new AddProductsToBasketViewModel()
            {
                BasketId = basketId,
                SupplierId = supplierId,
                RetailerId = _profileProvider.GetEntityId()
            };
            return PartialView("~/Views/Retailer/BrowseSuppliers/_AddProductsToBasket.cshtml", viewmodel);
        }

        public ActionResult DataTableTest()
        {
            var result = _productApi.GetSupplierProductsPaged(new Page
            {
                AdditionalParams = new Dictionary<string, string>{{"SupplierId","2"}},
                OrderBy = string.Empty,
                OrderDirection = 1,
                PageNumber = 1,
                PageSize = 10,
                SearchText = string.Empty

            });
            return View("~/Views/Retailer/BrowseSuppliers/DataTableTest.cshtml");
        }
    }
}
