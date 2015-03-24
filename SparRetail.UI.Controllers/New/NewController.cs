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
            if (retailer == null)
                throw new NullReferenceException("Retailer cannot be null");
            if (retailerApi == null)
                throw new NullReferenceException("RetailerApi cannot be null");

            
            var result = retailerApi.Create(new Models.Retailer() { RetailerName = retailer.RetailerName });
            if (result == null)
                throw new NullReferenceException("Result cannnot be null");
            if (result.IsCallSuccess && result.IsCommandSuccess)
            {
                // TODO: associate user with retailer.
                if(User == null)
                    throw new NullReferenceException("User cannot be null");
                if(User.Identity == null)
                    throw new NullReferenceException("UserIdendity cannot be null");

                var profile = ProfileBase.Create(User.Identity.Name, true);
                if (profile == null)
                    throw new NullReferenceException("Profile cannot be null");
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
