using Autofac;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SparRetail.Suppliers
{
    public class IoCRegistry
    {
        public static void Configure(ContainerBuilder builder)
        {
            builder.RegisterType<SupplierRepository>().As<ISupplierRepository>().SingleInstance();
            builder.RegisterType<SupplierService>().As<ISupplierService>().SingleInstance();

        }
    }
}
