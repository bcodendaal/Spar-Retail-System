using Autofac;
using SparRetail.Products.Repositories;
using SparRetail.Products.Services;

namespace SparRetail.Products
{
    public static class IoCRegistry
    {
        public static void Configure(ContainerBuilder builder)
        {
            builder.RegisterType<ProductRepository>().As<IProductRepository>().SingleInstance();
            builder.RegisterType<ProductService>().As<IProductService>().SingleInstance();
            DatabaseConfigAdapter.IoCRegistry.Configure(builder);
        }
    }
}