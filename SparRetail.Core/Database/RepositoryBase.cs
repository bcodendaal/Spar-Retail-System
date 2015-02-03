using SparRetail.Core.Config;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using System.Data.SqlClient;
using SparRetail.Core.Constants;

namespace SparRetail.Core.Database
{
    public class RepositoryBase
    {
        protected IDatabaseConfigCollection ConfigCollection;

        public RepositoryBase(IDatabaseConfigCollection configCollection)
        {
            this.ConfigCollection = configCollection;
        }
        
        protected IDatabaseConfigItem GetConfig(string databaseConfigKey)
        {
            if (ConfigCollection.KeyExists(databaseConfigKey))
                return ConfigCollection.Get(databaseConfigKey);
            else
                return LoadConfig(databaseConfigKey);
         }

        private IDatabaseConfigItem LoadConfig(string databaseConfigKey)
        {
            using (SqlConnection connection = new SqlConnection(GetConfig(CommonConfigKeys.dbKeyMaster).ConnectionString))
            {
                var result = connection.Query<DatabaseConfigItem>("usp_GetDatabaseConfigItem", new { @ConfigKey = databaseConfigKey }, commandType: System.Data.CommandType.StoredProcedure);
                if (result != null && result.Any())
                {
                    ConfigCollection.Add(result.First());
                    return result.First();
                }
                else
                    throw new KeyNotFoundException("Could not find database config key: " + databaseConfigKey);                    
            }
        }

        protected List<T> QueryList<T>(string storedProcedure, object parameters, string databaseConfigKey) where T : class
        {
            using (SqlConnection connection = new SqlConnection(GetConfig(databaseConfigKey).ConnectionString))
            {
                return connection.Query<T>(storedProcedure, parameters, null, false, GetConfig(databaseConfigKey).CommandTimeout, System.Data.CommandType.StoredProcedure).ToList();                
            }
        }

        protected T QueryOne<T>(string storedProcedure, dynamic parameters, string databaseConfigKey)
        {
            using (SqlConnection connection = new SqlConnection(GetConfig(databaseConfigKey).ConnectionString))
            {
                var result = SqlMapper.Query<T>(connection, storedProcedure, parameters, null, false, GetConfig(databaseConfigKey).CommandTimeout, System.Data.CommandType.StoredProcedure);
                if (result != null && result.Any())
                    return result.First();
                return default(T);
            }
        }

        protected void Execute(string storedProcedure, dynamic parameters, string databaseConfigKey)
        {
            using (SqlConnection connection = new SqlConnection(GetConfig(databaseConfigKey).ConnectionString))
            {
                SqlMapper.Execute(connection, storedProcedure, parameters, null, GetConfig(databaseConfigKey).CommandTimeout, System.Data.CommandType.StoredProcedure);
            }
        }
        
    }
}
