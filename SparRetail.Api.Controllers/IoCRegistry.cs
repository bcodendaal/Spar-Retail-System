using Autofac;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace SparRetail.Api.Controllers
{
    public static class IoCRegistry
    {
        public static void Configure(ContainerBuilder builder)
        {
            Core.IoCRegistry.Configure(builder);
            Suppliers.IoCRegistry.Configure(builder);
            Products.IoCRegistry.Configure(builder);
            Orders.IoCRegistry.Configure(builder);
            Retailers.IoCRegistry.Configure(builder);
        }

        public static Assembly GetAssembly()
        {
            return Assembly.GetExecutingAssembly();
        }

        
    }
}
