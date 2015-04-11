using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using SparRetail.Core;
using SparRetail.Core.Config;
using SparRetail.Core.Constants;
using SparRetail.Core.Messaging;
using Autofac;
using SparRetail.Components.MailMan;
using SparRetail.PlugIns.Interop;

namespace SparRetail.PlugIns.MailMan.ConsoleHost
{
    class Program
    {
        public static void Main(string[] args)
        {
            RegisterIoC();
            IoC.Container.Resolve<IMailManWorker>();
            Console.WriteLine("Attempting to start mail man host...");
            var host = IoC.Container.Resolve<IHost>();
            host.Start();

            Console.WriteLine("Mail man host has started");
            Console.ReadKey();
            Console.WriteLine("Attempting to shut down mail man host...");
            host.Stop();
            Console.WriteLine("Mail man host has been shut down. Exiting in 10 seconds");
            Thread.Sleep(10000);

        }

        private static void RegisterIoC()
        {
            IoC.BootStrap(builder =>
            {

                var encryption = new Encryption.EncryptionService();
                var dbConfig = new DatabaseConfigCollection();
                dbConfig.Add(new DatabaseConfigItem() { Key = CommonConfigKeys.dbKeyMaster, ConnectionString = encryption.Decrypt(ConfigurationManager.ConnectionStrings["master"].ConnectionString), CommandTimeout = 100 });
                builder.RegisterInstance(dbConfig).As<IDatabaseConfigCollection>().SingleInstance();
                SparRetail.Components.MailMan.IoCRegistry.Configure(builder, new ConfigCollection(new ConfigRepository()));
                builder.RegisterType<MailManHost>().As<IHost>().SingleInstance();
                builder.RegisterInstance(new QueueSettings { QueueName = "mailman", RoutingKey = "#" });
                builder.RegisterType<MailManPlugIn>().As<MailManPlugIn>().SingleInstance();
            });
        }
    }
}
