using Autofac;
using SparRetail.Core.Cache;
using SparRetail.Core.Config;
using SparRetail.Core.Email;
using SparRetail.Core.Logging;
using SparRetail.Core.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SparRetail.Core
{
    public static class IoCRegistry
    {
        public static void Configure(ContainerBuilder builder, IConfigCollection configCollection)
        {
            builder.RegisterType<InMemoryCacheRepository>().As<ICacheRepository>().SingleInstance();
            builder.RegisterType<CacheBroker>().As<ICacheBroker>().SingleInstance();
            builder.RegisterType<Logger>().As<ILogger>().SingleInstance();
            builder.RegisterInstance(configCollection).As<IConfigCollection>().SingleInstance();
            builder.RegisterType<ConfigRepository>().As<IConfigRepository>().SingleInstance();
            builder.RegisterType<Mailer>().As<IMailer>().SingleInstance();
            RegisterMessageHosts(builder, configCollection);
            RegisterMessageExchanges(builder, configCollection);

            builder.RegisterType<MessageProducer>().As<IMessageProducer>().SingleInstance();
        }

        private static void RegisterMessageHosts(ContainerBuilder builder, IConfigCollection configCollection)
        {
            var allMessageHosts = configCollection.All()
               .Where(x => x.Key.Contains("rabbit.host"))
               .Select(x => new ConfigItem() { Key = x.Key.Replace("rabbit.host.", ""), Value = x.Value });

            var hostTypes = GetTypes(allMessageHosts);

            hostTypes.ForEach(hostType =>
            {
                builder.RegisterInstance(new HostSettings()
                {
                    Key = GetConfigItem(allMessageHosts, hostType, "key"),
                    Host = GetConfigItem(allMessageHosts, hostType, "host"),
                    VHost  = GetConfigItem(allMessageHosts, hostType, "vhost"),
                    Password = GetConfigItem(allMessageHosts, hostType, "password"),
                    Username = GetConfigItem(allMessageHosts, hostType, "username"),
                    Port = Int32.Parse(GetConfigItem(allMessageHosts, hostType, "port"))
                    
                }).As<HostSettings>();
            });


        }

     

        private static void RegisterMessageExchanges(ContainerBuilder builder, IConfigCollection configCollection)
        {
            var allExchangeSettings = configCollection.All()
                .Where(x => x.Key.Contains("rabbit.exchange"))
                .Select(x => new ConfigItem() { Key = x.Key.Replace("rabbit.exchange.", ""), Value = x.Value });

            var exchangeTypes = GetTypes(allExchangeSettings);

            exchangeTypes.ForEach(exchangeType => 
            {
                builder.RegisterInstance(new ExchangeSettings() 
                {
                    Key = GetConfigItem(allExchangeSettings, exchangeType, "key"),
                    HostKey = GetConfigItem(allExchangeSettings, exchangeType, "hostkey"),
                    ExchangeName = GetConfigItem(allExchangeSettings, exchangeType, "exchangename"),
                    ExchangeType = GetConfigItem(allExchangeSettings, exchangeType, "exchangetype"),
                    Durable = Boolean.Parse(GetConfigItem(allExchangeSettings, exchangeType, "durable")),
                    AutoDelete = Boolean.Parse(GetConfigItem(allExchangeSettings, exchangeType, "autodelete")),
                    AutoCreate = Boolean.Parse(GetConfigItem(allExchangeSettings, exchangeType, "autocreate"))
                }).As<ExchangeSettings>();
            });

            

            
        
        }

        private static string GetConfigItem(IEnumerable<ConfigItem> allSettings, string type, string settingType)
        {
            var key = string.Format("{0}.{1}", settingType, type);
            if (allSettings.Any(x => x.Key == key))
                return allSettings.First(x => x.Key == key).Value;

            return string.Empty;
        }

        private static List<string> GetTypes(IEnumerable<ConfigItem> allSettings)
        {
            return allSettings.Select(x => x.Key.Substring(x.Key.IndexOf(".") + 1, x.Key.Length - x.Key.IndexOf(".") - 1)).Distinct().ToList();
        }
    }
}
