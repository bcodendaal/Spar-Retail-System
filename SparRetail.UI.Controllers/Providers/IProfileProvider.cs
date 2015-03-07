using SparRetail.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SparRetail.UI.Controllers.Providers
{
    public interface IProfileProvider
    {
        int GetEntityId();
        TenantType GetTenantType();
        TenantType GetTenantType(string userName);
    }
}
