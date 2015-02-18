using SparRetail.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SparRetail.Retailers.Repositories
{
    public interface IRetailerRepository
    {
        Retailer GetById(int retailerId);
    }
}
