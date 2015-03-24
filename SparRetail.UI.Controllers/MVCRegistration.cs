using SparRetail.UI.Controllers.Filters;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace SparRetail.UI.Controllers
{
    public static class MVCRegistration
    {
        public static void RegisterFilters(GlobalFilterCollection filters)
        {
            if (ConfigurationManager.AppSettings["useHttps"].ToLower() == "true")
                filters.Add(new RequireSecureConnectionFilter());
            filters.Add(new AuthorizeAttribute(), 1);
        }
    }
}
