using SparRetail.Models;
using SparRetail.Models.Api;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SparRetail.Interop
{
    public interface ISupplierApi
    {
        List<Supplier> All();
        CreateSupplierResponse Create(Supplier supplier);
        Page<Supplier> GetAllSuppliersForRetailerPaged(SupplierPagedParams pageParam);
        Supplier GetSupplierById(int supplierId);
    }
}
