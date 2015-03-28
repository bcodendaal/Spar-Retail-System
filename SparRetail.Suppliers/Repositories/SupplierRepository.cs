using SparRetail.Core.Config;
using SparRetail.Core.Constants;
using SparRetail.Core.Database;
using SparRetail.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace SparRetail.Suppliers
{
    public class SupplierRepository : RepositoryBase, ISupplierRepository
    {
        public SupplierRepository(IDatabaseConfigCollection configCollection)
            : base(configCollection)
        {

        }

        public List<Supplier> All()
        {
            return QueryList<Supplier>("usp_SelectAllSuppliers", null, CommonConfigKeys.dbKeyMaster);
        }

        public Supplier GetById(int supplierId)
        {
            return QueryOne<Supplier>("usp_SelectSupplierById", new { @SupplierId = supplierId }, CommonConfigKeys.dbKeyMaster);
        }


        public Supplier Create(Supplier supplier)
        {
            return QueryOne<Supplier>("usp_InsertSupplier", new { @SupplierName = supplier.SupplierName, @DatabaseConfigKey = supplier.DatabaseConfigKey }, CommonConfigKeys.dbKeyMaster);
        }


        public Page<Supplier> GetAllSuppliersForRetailerPaged(SupplierPagedParams pageParam)
        {

            return QueryMultiple("usp_SelectAllSuppliersForRetailerPaged",
                new
                {
                    @RetailerId = pageParam.RetailerId,
                    @SearchText = pageParam.SearchText,
                    @OrderBy = pageParam.OrderBy,
                    @OrderDirection = pageParam.OrderDirection,
                    @PageNumber = pageParam.PageNumber,
                    @PageSize = pageParam.PageSize
                },
                new Func<List<Supplier>, Page, Page<Supplier>>(
                    (list, page) => new Page<Supplier>(page, list)),
                CommonConfigKeys.dbKeyMaster);
        }
    }
}
