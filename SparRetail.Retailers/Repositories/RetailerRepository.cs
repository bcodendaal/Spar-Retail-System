using SparRetail.Core.Config;
using SparRetail.Core.Constants;
using SparRetail.Core.Database;
using SparRetail.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SparRetail.Retailers.Repositories
{
    public class RetailerRepository : RepositoryBase,  IRetailerRepository
    {
        public RetailerRepository(IDatabaseConfigCollection config) : base(config)
        { }

        public Retailer GetById(int retailerId)
        {
            return QueryOne<Retailer>("usp_SelectRetailerById", new { @RetailerId = retailerId }, CommonConfigKeys.dbKeyMaster);
        }


        public Retailer Create(Retailer retailer)
        {
            return QueryOne<Retailer>("usp_InsertRetailer", new 
            { 
            	@DatabaseConfigKey = retailer.DatabaseConfigKey,
	            @RetailerCode = retailer.RetailerCode,
	            @StoreCode = retailer.StoreCode,
	            @RetailerName = retailer.RetailerName,
	            @AddressLine1 = retailer.AddressLine1,
	            @AddressLine2 = retailer.AddressLine2,
	            @AddressLine3 = retailer.AddressLine3,
	            @City = retailer.City,
	            @Telephone = retailer.Telephone,
	            @Fax = retailer.Fax,
	            @Email = retailer.Email,
	            @Province = retailer.Province,
	            @PostalCode	 = retailer.PostalCode,
	            @VatNumber = retailer.VatNumber
            }, CommonConfigKeys.dbKeyMaster);
        }
    }
}
