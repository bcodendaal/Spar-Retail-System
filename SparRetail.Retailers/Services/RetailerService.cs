using SparRetail.Core.Cache;
using SparRetail.Models;
using SparRetail.Retailers.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SparRetail.Retailers.Services
{
    public class RetailerService : IRetailerService
    {
        protected readonly IRetailerRepository retailerRepository;
        protected readonly ICacheBroker cacheBroker;

        public RetailerService(IRetailerRepository retailerRepository, ICacheBroker cacheBroker)
        {
            this.retailerRepository = retailerRepository;
            this.cacheBroker = cacheBroker;
        }

        public Retailer GetById(int retailerId)
        {
            return cacheBroker.TryGet<Retailer>(string.Format(CacheKeys.RetailerCacheKey, retailerId), () => retailerRepository.GetById(retailerId), Volatility.Low);
        }
    }
}
