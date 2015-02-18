using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SparRetail.Models
{
    public class Retailer
    {
        public int RetailerId { get; set; }
        public string DatabaseConfigKey { get; set; }
        public string RetailerCode { get; set; }
        public string StoreCode { get; set; }
        public string RetailerName { get; set; }
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public string AddressLine3 { get; set; }
        public string City { get; set; }
        public string Telephone { get; set; }
        public string Fax { get; set; }
        public string Email { get; set; }
        public string Province { get; set; }
        public string PostalCode { get; set; }
        public string VatNumber { get; set; }        
        public DateTime CreatedOn { get; set; }
    }
}
