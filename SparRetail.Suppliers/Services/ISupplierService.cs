﻿using SparRetail.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SparRetail.Suppliers
{
    public interface ISupplierService
    {
        List<Supplier> All();
        Supplier GetById(int supplierId);
        CommandResponse<Supplier> Create(Supplier supplier);
        Page<Supplier> GetAllSuppliersForRetailerPaged(SupplierPagedParams pageParam);
    }
}
