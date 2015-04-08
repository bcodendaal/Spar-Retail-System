using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SparRetail.Core.Config
{
    public class ConfigCollection : IConfigCollection
    {
        protected List<ConfigItem> AllItems;
        protected IConfigRepository configRepository;

        public ConfigCollection(IConfigRepository configRepository)
        {
            this.configRepository = configRepository;
            AllItems = configRepository.GetAll();
        }

        public string Get(string configKey)
        {
            if (AllItems.All(x => x.Key != configKey))
                throw new KeyNotFoundException(string.Format("Could not find config key: {0}", configKey));

            return AllItems.First(x => x.Key == configKey).Value;
        }

        public List<ConfigItem> All()
        {
            return AllItems;
        }
    }
}
