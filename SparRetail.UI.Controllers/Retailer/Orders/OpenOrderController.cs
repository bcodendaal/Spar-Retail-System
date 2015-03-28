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

        public ActionResult All()
        {

            return View(@"~\Views\Retailer\Orders\OpenOrders\AllOpenOrders.cshtml");
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
                recordsFiltered = result.RowCount,
                recordsTotal = result.RowCount
            },
                            JsonRequestBehavior.AllowGet);
        }

        public ActionResult AddProducts(int orderId)
        {
            var order = _orderApi.GetOpenOrderDetail(orderId, _profileProvider.GetEntityId());
            var model = new ProductsOrderViewModel()
            {
                Order = order,
                Supplier = _supplierApi.GetSupplierById(order.SupplierId)
            };

            return View("~/Views/Retailer/Orders/OpenOrders/AddProducts.cshtml", model);
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
                recordsFiltered = result.RowCount,
                recordsTotal = result.RowCount
            },
                            JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetOpenOrderTotals(int orderId)
        {
            var orderDetails = _orderApi.GetOpenOrderDetail(orderId, _profileProvider.GetEntityId());
            if (orderDetails != null)
            {

                return Json(new { TotalPrice = orderDetails.TotalPrice, TotalProducts = orderDetails.TotalProducts },
                    JsonRequestBehavior.AllowGet);
            }
            return Json(new { TotalPrice = 0, TotalProducts = 0 },
                    JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Adds a product to an Open Order
        /// If an item already exists for that order the user has to decide to update the product quantity or keep the old quantity
        /// </summary>
        /// <param name="basketId"></param>
        /// <param name="productId"></param>
        /// <param name="supplierId"></param>
        /// <param name="quantity"></param>
        /// <returns></returns>
        public JsonResult AddItemToBasket(int basketId, int productId, int supplierId, int quantity)
        {

            var basketProducts = _orderApi.AllItemsForOrderBasket(basketId, _profileProvider.GetEntityId());

            //check if item already exists
            if (basketProducts.Any(x => x.ProductId == productId))
            {
                return Json(new { Success = true, DuplicateProduct = true, ProductQuantity = basketProducts.First(x => x.ProductId == productId).NumberOfUnits }, JsonRequestBehavior.AllowGet);
            }
            else // if item does not exits, add it to Open Order
            {
                var supplier = _supplierApi.All().FirstOrDefault(x => x.SupplierId == supplierId);

                var product = _productApi.GetAllForSupplier(supplier).FirstOrDefault(x => x.ProductId == productId);


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

                return Json(new { Success = true, DuplicateProduct = false, ProductQuantity = 0 }, JsonRequestBehavior.AllowGet);
                //ToDo: Refactor So that Totals are returned
            }
        }


        public JsonResult UpdateProductQuantity(int basketId, int productId, int supplierId, int quantity)
            {


            var supplier = _supplierApi.All().FirstOrDefault(x => x.SupplierId == supplierId);
            var product =
                _orderApi.AllItemsForOrderBasket(basketId, _profileProvider.GetEntityId()).First(x => x.ProductId == productId);
            if (quantity == 0)
            {
                _orderApi.DeleteOrderBasketItem(new OrderBasketItemPost()
                {
                    OrderBasketItem = product,
                    RetailerId = _profileProvider.GetEntityId()
                });
            }
            else
            {
                _orderApi.UpdateOrderBasketItem(new OrderBasketItemPost()
                {
                    RetailerId = _profileProvider.GetEntityId(),
                    OrderBasketItem = new OrderBasketItem()
                    {
                        OrderBasketId = basketId,
                        RetailerOrderBasketItemId = product.RetailerOrderBasketItemId,
                        BarCode = product.BarCode,
                        ProductId = product.ProductId,
                        NumberOfUnits = quantity,
                        PricePerUnit = product.PricePerUnit,
                        ProductCode = product.ProductCode,
                        ProductName = product.ProductName,
                        TotalPrice = quantity * product.PricePerUnit,
                        UnitOfMeasure = product.UnitOfMeasure
                    }
                });
            }
            return Json(new { Success = true }, JsonRequestBehavior.AllowGet);
            //ToDo: Refactor So that Totals are returned
        }


        public ActionResult OrderDetail(int OpenOrderId)
        {
            var order =
                _orderApi.GetOpenOrderDetail(OpenOrderId, _profileProvider.GetEntityId());

            var viewmodel = new OpenOrderDetailViewModel()
            {
                OrderDetails = order,
                Supplier = _supplierApi.GetSupplierById(order.SupplierId)
            };
            return View(@"~\Views\Retailer\Orders\OpenOrders\OrderDetails.cshtml", viewmodel);
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

        public JsonResult OpenOrderItemDataTableAjax(DataTableParam param)
        {
            var result = _orderApi.GetOpenOrderItemsPaged(new OpenOrderItemPageParams()
            {
                RetailerId = _profileProvider.GetEntityId(),
                OpenOrderId = Convert.ToInt32(param.additionalParams["OpenOrderId"]),
                OrderBy = param.OrderBy,
                OrderDirection = param.OrderDirection,
                PageNumber = param.PageNumber,
                PageSize = param.length,
                SearchText = param.SearchText

            });

            return Json(new DataTableJsonReturnModel<OrderBasketItem>()
            {
                data = result.Results,
                draw = Convert.ToInt32(param.draw),
                error = string.Empty,
                recordsFiltered = result.RowCount,
                recordsTotal = result.RowCount
            },
                            JsonRequestBehavior.AllowGet);
        }
    }
}
