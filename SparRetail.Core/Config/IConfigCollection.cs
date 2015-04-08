using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SparRetail.Core.Config
{
    public interface IConfigCollection
    {
        string Get(string configKey);
        List<ConfigItem> All();
    }
}
