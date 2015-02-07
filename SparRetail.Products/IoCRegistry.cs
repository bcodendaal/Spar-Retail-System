using Autofac;
using SparRetail.Products.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autofac;
using SparRetail.Products.Services;

namespace SparRetail.Products
{
    public static class IoCRegistry
    {
        public static void Configure(ContainerBuilder builder)
        {
            builder.RegisterType<ProductRepository>().As<IProductRepository>().SingleInstance();
            builder.RegisterType<ProductService>().As<IProductService>().SingleInstance();
        }
    }
}
