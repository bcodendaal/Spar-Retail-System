using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using Spar.Retail.UI.Models.ViewModels;
using Spar.Retail.UI.Models.ViewModels.Retailer.Orders;
using SparRetail.Interop;
using SparRetail.Models;
using SparRetail.Models.Api;
using SparRetail.UI.Controllers.Providers;

namespace SparRetail.UI.Controllers.Retailer.Orders
{
    public class OpenOrderController : Controller
    {
        private ISupplierApi _supplierApi;
        private IProductApi _productApi;
        private IOrderApi _orderApi;
        private IProfileProvider _profileProvider;

        public OpenOrderController(ISupplierApi supplierApi, IProductApi productApi, IOrderApi orderApi,
            IProfileProvider profileProvider)
        {
            _supplierApi = supplierApi;
            _productApi = productApi;
            _orderApi = orderApi;
            _profileProvider = profileProvider;
        }

        public ActionResult OpenOrders()
        {

            return View(@"~\Views\Retailer\Orders\OpenOrders\OpenOrders.cshtml");
        }

        public JsonResult OpenOrdersDataTableAjax(DataTableParam param)
        {
            var result = _orderApi.AllOpenOrdersForRetailerPaged(new OpenOrderPageParams
            {
                RetailerId = _profileProvider.GetEntityId(),
                OrderBy = param.OrderBy,
                OrderDirection = param.OrderDirection,
                PageNumber = param.PageNumber,
                PageSize = param.length,
                SearchText = param.SearchText

            });

            return Json(new DataTableJsonReturnModel<OpenOrderPageResult>()
            {
                data = result.Results,
                draw = Convert.ToInt32(param.draw),
                error = string.Empty,
                recordsFiltered = result.Results.Count,
                recordsTotal = result.TotalRows
            },
                            JsonRequestBehavior.AllowGet);
        }

        public ActionResult AddProducts(int orderId)
        {
            var order =
                _orderApi.AllOrderBasketForRetailer(_profileProvider.GetEntityId())
                    .First(x => x.OrderBasketId == orderId);
            var model = new ProductsOrderViewModel()
            {
                Order = order,
                Supplier = _supplierApi.GetSupplierById(order.SupplierId)
            };

            return View("~/Views/Retailer/Orders/AddProducts.cshtml", model);
        }

        public JsonResult ProductsDataTableAjax(DataTableParam param)
        {
            var result = _productApi.GetSupplierProductsPaged(new ProductPagedParams
            {
                SupplierId = Convert.ToInt32(param.additionalParams["SupplierId"]),
                OrderBy = param.OrderBy,
                OrderDirection = param.OrderDirection,
                PageNumber = param.PageNumber,
                PageSize = param.length,
                SearchText = param.SearchText

            });

            return Json(new DataTableJsonReturnModel<Product>()
            {
                data = result.Results,
                draw = Convert.ToInt32(param.draw),
                error = string.Empty,
                recordsFiltered = result.Results.Count,
                recordsTotal = result.TotalRows
            },
                            JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetOpenOrderTotals(int orderId)
        {
            var orderDetails = _orderApi.GetOpenOrderTotals(orderId, _profileProvider.GetEntityId());
            if (orderDetails != null)
            {

                return Json(new { TotalPrice = orderDetails.TotalPrice, TotalProducts = orderDetails.TotalProducts },
                    JsonRequestBehavior.AllowGet);
            }
            return Json(new { TotalPrice = 0, TotalProducts = 0 },
                    JsonRequestBehavior.AllowGet);
        }

        public JsonResult AddItemToBasket(int basketId, int basketItemId, int supplierId, int quantity)
        {
            var supplier = _supplierApi.All().FirstOrDefault(x => x.SupplierId == supplierId);
            var product =
                _productApi.GetAllForSupplier(supplier).FirstOrDefault(x => x.ProductId == basketItemId);

            _orderApi.AddOrderBasketItem(new OrderBasketItemPost()
            {
                RetailerId = _profileProvider.GetEntityId(),
                OrderBasketItem = new OrderBasketItem()
                {
                    OrderBasketId = basketId,
                    BarCode = product.Barcode,
                    ProductId = product.ProductId,
                    NumberOfUnits = quantity,
                    PricePerUnit = product.Price,
                    ProductCode = product.ProductCode,
                    ProductName = product.ProductName,
                    TotalPrice = quantity * product.Price,
                    UnitOfMeasure = product.UnitOfMeasureName
                }
            });
            return Json(new { TotalPrice = 0, TotalProducts = 0 },
                    JsonRequestBehavior.AllowGet);
            //ToDo: Refactor So that Totals are returned
        }

        public JsonResult FinalizeOrder(int orderId)
        {
            try
            {
                var result = _orderApi.FinaliseOrder(new FinaliseOrderPost()
                {
                    OrderBasketId = orderId,
                    RetailerId = _profileProvider.GetEntityId()
                });

                if (result.IsCommandSuccess)
                {
                    return Json(new { Success = true }, JsonRequestBehavior.AllowGet);
                }
                return Json(new { Success = false }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                return Json(new { Success = false }, JsonRequestBehavior.AllowGet);
            }
        }
    }
}
