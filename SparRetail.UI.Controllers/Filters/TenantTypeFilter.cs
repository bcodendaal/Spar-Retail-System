using SparRetail.Models.Enums;
using SparRetail.UI.Controllers.Providers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace SparRetail.UI.Controllers.Filters
{
    public class TenantTypeFilter : AuthorizeAttribute
    {
        private readonly TenantType tenantType;
        public IProfileProvider profileProvider;
        public TenantTypeFilter(TenantType tenantType)
        {
            this.tenantType = tenantType;
            profileProvider = DependencyResolver.Current.GetService<IProfileProvider>();
        }

        protected override bool AuthorizeCore(System.Web.HttpContextBase httpContext)
        {
            if (profileProvider.GetTenantType() != tenantType)
                return false;
            return true;
        }
        

        
    }
}
