using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

namespace SparRetail.Core.Messaging
{
    public class MessageProducer : IMessageProducer
    {
        protected List<HostSettings> HostSettings;
        protected List<ExchangeSettings> ExchangeSettings;
        protected ConcurrentDictionary<string, RabbitHostConnection> ConnectionFactories = new ConcurrentDictionary<string,RabbitHostConnection>();
        

        public MessageProducer(IEnumerable<HostSettings> hostSettings, IEnumerable<ExchangeSettings> exchangeSettings)
        {
            HostSettings = hostSettings.ToList();
            ExchangeSettings = exchangeSettings.ToList();
            Initialize();
        }

        private void Initialize()
        {
            if (HostSettings != null)
            {
                HostSettings.ForEach(x=>
                {
                    ConnectionFactories.TryAdd(x.Key, new RabbitHostConnection(x));
                    ConnectionFactories[x.Key].Initialize();
                });
            }
            if (ExchangeSettings != null)
            {
                ExchangeSettings.ForEach(x => 
                {
                    if (x.AutoCreate && ConnectionFactories.ContainsKey(x.HostKey))
                    {
                        ConnectionFactories[x.HostKey].DeclareExchange(x);
                    }
                });
            }
        }

        public void PublishMessage(string exchangeSettingsKey, List<object> messages, string routingKey, Dictionary<string, object> headers = null)
        {
            if (ExchangeSettings.Any(x => x.Key == exchangeSettingsKey) && ConnectionFactories.ContainsKey(ExchangeSettings.First(x => x.Key == exchangeSettingsKey).HostKey))
            {
                ConnectionFactories[ExchangeSettings.First(x => x.Key == exchangeSettingsKey).HostKey]
                    .PublishMessage(ExchangeSettings.First(x => x.Key == exchangeSettingsKey), messages.Select(JsonConvert.SerializeObject).ToList(), routingKey, headers);
            }
        }
    }
}
