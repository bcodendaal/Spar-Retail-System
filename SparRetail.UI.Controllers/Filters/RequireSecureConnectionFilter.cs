using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web;

namespace SparRetail.UI.Controllers.Filters
{
    public class RequireSecureConnectionFilter : RequireHttpsAttribute
    {
        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            if (filterContext == null)
            {
                throw new ArgumentNullException("Filter context");
            }

            if(filterContext.HttpContext.Request.IsLocal)
            {
                return;
            }

            
            base.OnAuthorization(filterContext);
        }
    }
}
