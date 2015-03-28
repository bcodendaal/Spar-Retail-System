using Autofac;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace SparRetail.Components.Products
{
    public class IoCRegistry
    {
        public static void Configure(ContainerBuilder builder)
        {
            builder.RegisterType<ProductsComponent>().As<IProductsComponent>().SingleInstance();
        }
    }
}
