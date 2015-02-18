using SparRetail.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SparRetail.Suppliers
{
    public class SupplierService : ISupplierService
    {
        protected ISupplierRepository supplierRepository;

        public SupplierService(ISupplierRepository supplierRepository)
        {
            this.supplierRepository = supplierRepository;
        }

        public List<Supplier> All()
        {
            return supplierRepository.All();
        }


        public Supplier GetById(int supplierId)
        {
            return supplierRepository.GetById(supplierId);
        }
    }
}
