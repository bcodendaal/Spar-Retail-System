using Spar.Retail.UI.Models.ViewModels.Retailer.Common;
using Spar.Retail.UI.Models.ViewModels.SupplierCommon;
using SparRetail.Interop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web.Profile;
using System.Web.Security;


namespace SparRetail.UI.Controllers.New
{
    public class NewController : Controller
    {
        protected readonly IRetailerApi retailerApi;
        protected readonly ISupplierApi supplierApi;

        public NewController(IRetailerApi retailerApi, ISupplierApi supplierApi)
        {
            this.retailerApi = retailerApi;
            this.supplierApi = supplierApi;
        }

        public ActionResult Select()
        {
            return View();
        }

        public ActionResult Retailer()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Retailer(NewRetailerViewModel retailer)
        {
            var result = retailerApi.Create(new Models.Retailer() { RetailerName = retailer.RetailerName });
            if (result.IsCallSuccess && result.IsCommandSuccess)
            {
                // TODO: associate user with retailer.
                var profile = ProfileBase.Create(User.Identity.Name, true);
                var str = profile.GetPropertyValue("ProfileType");
                profile.SetPropertyValue("ProfileType", "Retailer");
                profile.SetPropertyValue("EntityId", result.RetailerId);
                profile.Save();

                return RedirectToAction("Index", "RetailerDashboard");
            }            

            return View();
        }

        [HttpPost]
        public ActionResult Supplier(NewSupplierViewModel supplier)
        {
            var result = supplierApi.Create(new Models.Supplier() { SupplierName = supplier.SupplierName });
            if (result.IsCallSuccess && result.IsCommandSuccess)
            {
                // TODO: associate user with retailer.
                var profile = ProfileBase.Create(User.Identity.Name, true);
                var str = profile.GetPropertyValue("ProfileType");
                profile.SetPropertyValue("ProfileType", "Supplier");
                profile.SetPropertyValue("EntityId", result.SupplierId);
                profile.Save();

                return RedirectToAction("Index", "SupplierDashboard");
            }

            return View();
        }

        public ActionResult Supplier()
        {
            return View();
        }
    }
}
