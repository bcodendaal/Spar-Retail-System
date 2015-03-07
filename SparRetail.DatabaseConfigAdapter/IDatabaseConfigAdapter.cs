using SparRetail.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SparRetail.DatabaseConfigAdapter
{
    public interface IDatabaseConfigAdapter
    {
        string GetRetailerDatabaseConfigKey(int retailerId);
        string GetSupplierDatabaseConfigKey(int supplierId);
        string GetLatestDatabaseConfig(DatabaseType type);
    }
}
