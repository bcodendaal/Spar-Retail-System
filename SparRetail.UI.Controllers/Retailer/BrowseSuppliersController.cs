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


namespace SparRetail.UI.Controllers.Retailer
{
    public class BrowseSuppliersController : Controller
    {
        private ISupplierApi _supplierApi;
        private IProductApi _productApi;
        private IOrderApi _orderApi;

        public BrowseSuppliersController(ISupplierApi supplierApi, IProductApi productApi, IOrderApi orderApi)
        {
            _supplierApi = supplierApi;
            _productApi = productApi;
            _orderApi = orderApi;
        }

        public ActionResult Index()
        {
            return View("~/Views/Retailer/BrowseSuppliers/Index.cshtml");
        }

        public ActionResult LoadOrderBasketOptions(int supplierId)
        {

            //check if any orders exists
            var orderbaskets = _orderApi.AllOrderBasketForRetailerSupplier(1, supplierId);
            if (orderbaskets.Any())
            {
                //if any orders exists user must choose one or choose to create a new order
                var viewmodel = new OrderBasketOptionsViewModel()
                {
                    OrderBaskets = orderbaskets,
                    RetailerId = 1,
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
            var response = _orderApi.CreateNew(supplierId, 1, 1);
            var viewmodel = new AddProductsToBasketViewModel()
            {
                BasketId = response.OrderBasket.OrderBasketId,
                SupplierId = supplierId,
                RetailerId = 1
            };
            return PartialView("~/Views/Retailer/BrowseSuppliers/_AddProductsToBasket.cshtml", viewmodel);
        }
    }
}
