using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SparRetail.Core.Config
{
    public class ConfigRepository : IConfigRepository
    {
        public List<ConfigItem> GetAll()
        {
            var lists = new List<ConfigItem>();
            foreach (var item in ConfigurationManager.AppSettings.AllKeys)
            {
                lists.Add(new ConfigItem { Key = item, Value = ConfigurationManager.AppSettings[item] });
            }

            return lists;
        }
    }
}
