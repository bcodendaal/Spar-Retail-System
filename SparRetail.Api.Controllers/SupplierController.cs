using SparRetail.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;


namespace SparRetail.Api.Controllers
{
    public class SupplierController : ApiController
    {
        public string Test()
        {
            return "Test works";
        }

        [HttpGet]
        public Supplier All()
        {
            return new Supplier();
        }
    }
}
