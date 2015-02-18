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
    }
}
