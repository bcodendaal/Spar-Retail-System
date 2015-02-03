using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SparRetail.Core.Config
{
    public interface IDatabaseConfigCollection
    {
        void Add(IDatabaseConfigItem configItem);
        void Add(List<IDatabaseConfigItem> configItems);
        bool KeyExists(string databaseConfigKey);
        IDatabaseConfigItem Get(string databaseConfigKey);
    }
}
