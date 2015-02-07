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
        public SupplierRepository(IDatabaseConfigCollection configCollection) : base(configCollection)
        {

        }

        public List<Supplier> All()
        {
            return QueryList<Supplier>("usp_SelectAllSuppliers", null, CommonConfigKeys.dbKeyMaster);
        }
    }
}
