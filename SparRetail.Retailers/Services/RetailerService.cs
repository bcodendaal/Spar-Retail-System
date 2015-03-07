using SparRetail.Core.Cache;
using SparRetail.DatabaseConfigAdapter;
using SparRetail.Models;
using SparRetail.Models.Enums;
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
        protected readonly IDatabaseConfigAdapter databaseConfigAdapter;

        public RetailerService(IRetailerRepository retailerRepository, IDatabaseConfigAdapter databaseConfigAdapter, ICacheBroker cacheBroker)
        {
            this.retailerRepository = retailerRepository;
            this.databaseConfigAdapter = databaseConfigAdapter;
            this.cacheBroker = cacheBroker;
        }

        public Retailer GetById(int retailerId)
        {
            return cacheBroker.TryGet<Retailer>(string.Format(CacheKeys.RetailerCacheKey, retailerId), () => retailerRepository.GetById(retailerId), Volatility.Low);
        }

        public CommandResponse<Retailer> Create(Retailer retailer)
        {
            try
            {
                // Todo: validation of retailer
                retailer.DatabaseConfigKey = databaseConfigAdapter.GetLatestDatabaseConfig(DatabaseType.Retailer);
                if (string.IsNullOrEmpty(retailer.DatabaseConfigKey))
                    return new CommandResponse<Retailer> { IsSuccess = false, Message = "Could not determine the next database config to use", Model = null };

                return new CommandResponse<Retailer> { IsSuccess = true, Message = "Success", Model = retailerRepository.Create(retailer) };
            }
            catch (Exception ex)
            {
                return new CommandResponse<Retailer> { IsSuccess = false, Model = null, Message = ex.ToString() };
            }
        }
    }
}
