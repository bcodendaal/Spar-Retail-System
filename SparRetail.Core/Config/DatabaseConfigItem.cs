using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SparRetail.Core.Config
{
    public class DatabaseConfigItem : IDatabaseConfigItem
    {
        public string Key { get; set; }
        public string ConnectionString { get; set; }
        public int CommandTimeout { get; set; }
    }
}
