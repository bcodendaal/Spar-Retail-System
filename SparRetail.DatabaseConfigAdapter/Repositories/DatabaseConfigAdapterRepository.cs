using SparRetail.Core.Config;
using SparRetail.Core.Constants;
using SparRetail.Core.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SparRetail.DatabaseConfigAdapter.Repositories
{
    public class DatabaseConfigAdapterRepository : RepositoryBase, IDatabaseConfigAdapterRepository
    {
        public DatabaseConfigAdapterRepository(IDatabaseConfigCollection config)
            : base(config)
        {
        }

        public string GetRetailerDatabaseConfigKey(int retailerId)
        {
            return QueryOne<string>("usp_SelectRetailerDatabaseConfigKey", new { @RetailerId = retailerId}, CommonConfigKeys.dbKeyMaster);
        }

        public string GetSupplierDatabaseConfigKey(int supplierId)
        {
            return QueryOne<string>("usp_SelectSupplierDatabaseConfigKey", new { @SupplierId = supplierId}, CommonConfigKeys.dbKeyMaster);
        }
    }
}
