using SparRetail.Core.Cache;
using SparRetail.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Profile;

namespace SparRetail.UI.Controllers.Providers
{
    public class ProfileProvider : IProfileProvider
    {
        protected ICacheBroker cacheBroker;
        public ProfileProvider(ICacheBroker cacheBroker)
        {
            this.cacheBroker = cacheBroker;
        }

        public int GetEntityId()
        {            
            var entityId = cacheBroker.TryGet<object>(string.Format(CacheKeys.UsernameEntityId,HttpContext.Current.User.Identity.Name), () => { var profile = ProfileBase.Create(HttpContext.Current.User.Identity.Name, true); return profile.GetPropertyValue("EntityId");}, Volatility.Low);
            return entityId != null ? (int)entityId : 0;
        }

        public TenantType GetTenantType()
        {
            return GetTenantType(HttpContext.Current.User.Identity.Name);
        }


        public TenantType GetTenantType(string userName)
        {
            //var type = cacheBroker.TryGet<string>(
            //    string.Format(CacheKeys.UsernameTenantType, HttpContext.Current.User.Identity.Name), 
            //    () => { 

            //    }, Volatility.Low);

            var profile = ProfileBase.Create(userName, true);
            var type = (string)profile.GetPropertyValue("ProfileType");

            return (TenantType)Enum.Parse(typeof(TenantType), type, true);
        }
    }
}
