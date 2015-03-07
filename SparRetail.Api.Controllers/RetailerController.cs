using SparRetail.Interop;
using SparRetail.Models;
using SparRetail.Models.Api;
using SparRetail.Retailers.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace SparRetail.Api.Controllers
{
    public class RetailerController : ApiController, IRetailerApi
    {
        protected readonly IRetailerService retailerService;

        public RetailerController(IRetailerService retailerService)
        {
            this.retailerService = retailerService;
        }
        public CreateRetailerResponse Create(Retailer retailer)
        {
            try
            {
                var result = retailerService.Create(retailer);
                return new CreateRetailerResponse { IsCommandSuccess = result.IsSuccess, Message = result.Message, IsCallSuccess = true, RetailerId = result.Model.RetailerId };
            }
            catch (Exception ex)
            {
                return new CreateRetailerResponse { IsCallSuccess = true, Message = ex.ToString(), IsCommandSuccess = false };                
            }
        }
    }
}
