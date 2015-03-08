using SparRetail.DatabaseConfigAdapter;
using SparRetail.Models;
using SparRetail.Models.Enums;
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
        protected IDatabaseConfigAdapter databaseConfigAdapter;

        public SupplierService(ISupplierRepository supplierRepository, IDatabaseConfigAdapter databaseConfigAdapter)
        {
            this.supplierRepository = supplierRepository;
            this.databaseConfigAdapter = databaseConfigAdapter;
        }

        public List<Supplier> All()
        {
            return supplierRepository.All();
        }

        public Supplier GetById(int supplierId)
        {
            return supplierRepository.GetById(supplierId);
        }

        public CommandResponse<Supplier> Create(Supplier supplier)
        {
            try
            {
                // Todo: validation of supplier
                supplier.DatabaseConfigKey = databaseConfigAdapter.GetLatestDatabaseConfig(DatabaseType.Supplier);
                if (string.IsNullOrEmpty(supplier.DatabaseConfigKey))
                    return new CommandResponse<Supplier> { IsSuccess = false, Message = "Could not determine the next database config to use", Model = null };

                return new CommandResponse<Supplier> { IsSuccess = true, Message = "Success", Model = supplierRepository.Create(supplier) };
            }
            catch (Exception ex)
            {
                return new CommandResponse<Supplier> { IsSuccess = false, Model = null, Message = ex.ToString() };
            }
        }
    }
}
