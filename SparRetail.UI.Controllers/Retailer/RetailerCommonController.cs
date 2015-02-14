using Spar.Retail.UI.Models.ViewModels.Retailer;
using SparRetail.Interop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using Spar.Retail.UI.Models.ViewModels.Retailer.Common;

namespace SparRetail.UI.Controllers.Retailer
{
    public class RetailerCommonController : Controller
    {

        private ISupplierApi _supplierApi;
        private IProductApi _productApi;
        private IOrderApi _orderApi;

        public RetailerCommonController(ISupplierApi supplierApi, IProductApi productApi, IOrderApi orderApi)
        {
            _supplierApi = supplierApi;
            _productApi = productApi;
            _orderApi = orderApi;
        }

        public ActionResult AllSuppliersOrder(string retailerId)
        {

            // Get All Suppliers for Retailer - neet to be more specific
            var viewmodel = new AllSupplierViewModel()
            {
                Suppliers = _supplierApi.All()
            };
            return PartialView("~/Views/Retailer/RetailerCommon/_AllSuppliersOrder.cshtml", viewmodel);
        }

        public ActionResult AllProductsOrder(int supplierId)
        {

            var supplier = _supplierApi.All().FirstOrDefault(x => x.SupplierId == supplierId);
            var viewmodel = new SupplierProductViewModel()
            {
                Supplier = supplier,
                Products = _productApi.GetAllForSupplier(supplier)
            };

            return PartialView(@"~\Views\Retailer\RetailerCommon\_AllSupplierProductsOrder.cshtml", viewmodel);
        }

        public ActionResult OrderBasket(int basketId)
        {
            var viewmodel = new OrderBasketViewModel()
            {
                Products = _orderApi.AllItemsForOrderBasket(basketId, 1),
                OrderBasket = _orderApi.AllOrderBasketForRetailer(1).FirstOrDefault(x => x.OrderBasketId == basketId)
            };

            return PartialView(@"~\Views\Retailer\RetailerCommon\OrderBasket.cshtml", viewmodel);
        }
    }
}
