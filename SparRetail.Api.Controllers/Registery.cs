using SparRetail.Api.Controllers.Handlers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace SparRetail.Api.Controllers
{
    public static class Registery
    {
        public static void ConfigureHandlers(HttpConfiguration config)
        {
            config.MessageHandlers.Add(new EnforceHttpsHandler());
        }
    }
}
