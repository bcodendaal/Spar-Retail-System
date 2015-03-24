﻿using Spar.Retail.UI.Models.ViewModels.Retailer;
using SparRetail.Interop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using Spar.Retail.UI.Models.ViewModels.Retailer.Common;
using SparRetail.Models;
using SparRetail.Models.Api;
using SparRetail.UI.Controllers.Providers;

namespace SparRetail.UI.Controllers.Retailer
{
    public class RetailerCommonController : Controller
    {

        private ISupplierApi _supplierApi;
        private IProductApi _productApi;
        private IOrderApi _orderApi;
        private IProfileProvider _profileProvider;
        public RetailerCommonController(ISupplierApi supplierApi, IProductApi productApi, IOrderApi orderApi, IProfileProvider profileProvider)
        {
            _supplierApi = supplierApi;
            _productApi = productApi;
            _orderApi = orderApi;
            _profileProvider = profileProvider;
        }

        public ActionResult MySuppliersOrder(string retailerId)
        {
            var viewmodel = new AllSupplierViewModel()
            {
                Suppliers = _supplierApi.All()
            };
            return PartialView("~/Views/Retailer/RetailerCommon/_MySuppliersOrder.cshtml", viewmodel);
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
                Products = _orderApi.AllItemsForOrderBasket(basketId, _profileProvider.GetEntityId()),
                OrderBasket = _orderApi.AllOrderBasketForRetailer(_profileProvider.GetEntityId()).FirstOrDefault(x => x.OrderBasketId == basketId)
            };

            return PartialView(@"~\Views\Retailer\RetailerCommon\OrderBasket.cshtml", viewmodel);
        }

        public ActionResult AllOpenOrderBaskets()
        {
            var orderBaskets = _orderApi.AllOrderBasketForRetailer(_profileProvider.GetEntityId());
            var viewmodel = new OrderBasketsViewModel()
            {
                OrderBaskets = new List<OrderBasketViewModel>()
            };

            foreach (var orderbasket in orderBaskets)
            {
                viewmodel.OrderBaskets.Add(new OrderBasketViewModel()
                {
                    OrderBasket = orderbasket,
                    Products = _orderApi.AllItemsForOrderBasket(orderbasket.OrderBasketId, _profileProvider.GetEntityId())
                });
            }

            return PartialView(@"~\Views\Retailer\RetailerCommon\_OrderBaskets.cshtml", viewmodel);
        }

        public ActionResult AddItemToBasket(int basketId, int basketItemId, int supplierId, int quantity)
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
            return OrderBasket(basketId);
        }

        public ActionResult UpdateBasketItemQuantity(int basketId, int basketitemid, int supplierId, int quantity)
        {
            if (quantity != 0)
            {
                var price = _orderApi.AllItemsForOrderBasket(basketId, _profileProvider.GetEntityId())
                    .FirstOrDefault(x => x.RetailerOrderBasketItemId == basketitemid)
                    .PricePerUnit;

                _orderApi.UpdateOrderBasketItem(new OrderBasketItemPost()
                {
                    RetailerId = _profileProvider.GetEntityId(),
                    OrderBasketItem = new OrderBasketItem()
                    {
                        RetailerOrderBasketItemId = basketitemid,
                        OrderBasketId = basketId,
                        NumberOfUnits = quantity,
                        TotalPrice = quantity * price
                    }
                });
            }
            // If quantity = 0, item should be deleted from basket
            else
            {
                _orderApi.DeleteOrderBasketItem(new OrderBasketItemPost()
                {
                    RetailerId = _profileProvider.GetEntityId(),
                    OrderBasketItem = new OrderBasketItem()
                    {
                        RetailerOrderBasketItemId = basketitemid,
                        OrderBasketId = basketId
                    }
                });
            }
            return OrderBasket(basketId);
        }
    }
}
