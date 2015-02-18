using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SparRetail.Core.Config
{
    public class DatabaseConfigCollection : IDatabaseConfigCollection
    {
        protected List<IDatabaseConfigItem> _configItems;

        public DatabaseConfigCollection()
        {
            _configItems = new List<IDatabaseConfigItem>();
        }

        public void Add(IDatabaseConfigItem configItem)
        {
            _configItems.Add(configItem);
        }

        public void Add(List<IDatabaseConfigItem> configItems)
        {
            _configItems.AddRange(configItems);
        }

        public bool KeyExists(string databaseConfigKey)
        {
            return _configItems.Any(x => x.Key == databaseConfigKey);
        }

        public IDatabaseConfigItem Get(string databaseConfigKey)
        {
            if (_configItems.Any(x => x.Key == databaseConfigKey))
                return _configItems.Single(x => x.Key == databaseConfigKey);
            else
                throw new KeyNotFoundException("Could not find database config key: " + databaseConfigKey);
        }
    }
}
