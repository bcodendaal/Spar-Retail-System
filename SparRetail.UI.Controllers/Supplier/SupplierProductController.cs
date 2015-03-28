using System;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using SparRetail.Interop;
using Spar.Retail.UI.Models.ViewModels.Supplier.Product;
using SparRetail.Models;
using System.Collections.Generic;
using log4net;
using SparRetail.UI.Controllers.Providers;

namespace SparRetail.UI.Controllers.Supplier
{
    public class SupplierProductController : Controller
    {
        protected readonly IProductApi ProductApi;
        protected readonly IProfileProvider ProfileProvider;
        protected readonly ILog Logger;
        

        public SupplierProductController(IProductApi productApi, IProfileProvider profileProvider)
        {
            ProductApi = productApi;
            ProfileProvider = profileProvider;
            Logger = LogManager.GetLogger(GetType());
        }

        public ActionResult Index()
        {
            return View(@"~\Views\Supplier\SupplierProduct\Index.cshtml");
        }

        public ActionResult AddProduct()
        {
            return PartialView(@"~\Views\Supplier\SupplierProduct\AddProduct.cshtml");
        }

        public ActionResult AllProducts()
        {
            int supplierId = ProfileProvider.GetEntityId();
            var allProducts = ProductApi.GetAllForSupplier(new Models.Supplier { SupplierId = supplierId });
            return PartialView(@"~\Views\Supplier\SupplierProduct\AllProducts.cshtml", new AllProductViewModel { Products = allProducts });
        }

        [HttpPost]
        public ActionResult AddProduct(AddProductViewModel addProductViewModel)
        {
            try
            {
                ProductApi.AddProducts(new List<Product>() { addProductViewModel.Product });
                
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
            }
            return Index();

        }
    }
}