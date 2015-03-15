using SparRetail.Core;
using SparRetail.Core.Config;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Autofac;
using SparRetail.Components.OrderProcessor;
using SparRetail.PlugIns.Interop;
using SparRetail.Core.Constants;
using System.Configuration;

namespace SparRetail.PlugIns.OrderBasketFinalize.ConsoleHost
{
    public class Program
    {
        public static void Main(string[] args)
        {
            RegisterIoC();
            IoC.Container.Resolve<IOrderProcessorWorker>();
            Console.WriteLine("Attempting to start order basket finalise host...");
            var host = IoC.Container.Resolve<IHost>();
            host.Start();

            Console.WriteLine("Order basket finalize host has started");
            Console.ReadKey();
            Console.WriteLine("Attempting to shut down order basket finalize host...");
            host.Stop();
            Console.WriteLine("Order basket finalize host has been shut down. Exiting in 10 seconds");
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
                SparRetail.Components.OrderProcessor.IoCRegistry.Configure(builder);
                builder.RegisterType<OrderBasketFinalizeHost>().As<IHost>().SingleInstance();
            });
        }
    }
}
