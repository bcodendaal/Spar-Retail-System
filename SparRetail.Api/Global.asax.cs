using SparRetail.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Routing;
using Autofac.Integration.WebApi;
using Autofac;
using SparRetail.Core.Config;
using SparRetail.Core.Constants;
using System.Configuration;

namespace SparRetail.Api
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            GlobalConfiguration.Configure(WebApiConfig.Register);

            IoC.BootStrap(builder => 
            {
                var encryption = new Encryption.EncryptionService();
                var dbConfig = new DatabaseConfigCollection();
                dbConfig.Add(new DatabaseConfigItem() { Key = CommonConfigKeys.dbKeyMaster, ConnectionString = encryption.Decrypt(ConfigurationManager.ConnectionStrings["master"].ConnectionString), CommandTimeout = 100 });
                builder.RegisterInstance(dbConfig).As<IDatabaseConfigCollection>().SingleInstance();
                builder.RegisterApiControllers(SparRetail.Api.Controllers.IoCRegistry.GetAssembly()).InstancePerRequest();
                SparRetail.Api.Controllers.IoCRegistry.Configure(builder);
            });

            
            GlobalConfiguration.Configuration.DependencyResolver = new AutofacWebApiDependencyResolver(IoC.Container);
        }
    }
}
