using SparRetail.Core.Cache;
using SparRetail.DatabaseConfigAdapter.Repositories;
using SparRetail.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SparRetail.DatabaseConfigAdapter
{
    public class DatabaseConfigAdapter : IDatabaseConfigAdapter
    {
        protected readonly IDatabaseConfigAdapterRepository databaseConfigAdapterRepository;
        protected readonly ICacheBroker cacheBroker;

        public DatabaseConfigAdapter(IDatabaseConfigAdapterRepository databaseConfigAdapterRepository, ICacheBroker cacheBroker)
        {
            this.databaseConfigAdapterRepository = databaseConfigAdapterRepository;
            this.cacheBroker = cacheBroker;
        }

        public string GetRetailerDatabaseConfigKey(int retailerId)
        {
            return cacheBroker.TryGet<string>(string.Format(CacheKeys.RetailerConfigKey, retailerId.ToString()), ()=> databaseConfigAdapterRepository.GetRetailerDatabaseConfigKey(retailerId), Volatility.Low);
        }

        public string GetSupplierDatabaseConfigKey(int supplierId)
        {
            return cacheBroker.TryGet<string>(string.Format(CacheKeys.SupplierConfigKey, supplierId.ToString()), () => databaseConfigAdapterRepository.GetSupplierDatabaseConfigKey(supplierId), Volatility.Low);
        }

        public string GetLatestDatabaseConfig(DatabaseType type)
        {
            return databaseConfigAdapterRepository.GetLatestDatabaseConfig(type);
        }
    }
}
