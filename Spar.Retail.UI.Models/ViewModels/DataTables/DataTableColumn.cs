using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spar.Retail.UI.Models.ViewModels
{
    public class DataTableColumn
    {
        /// <summary>
        /// Column's data source, as defined by columns.dataDT.
        /// </summary>
        public string data { get; set; }

        /// <summary>
        /// Column's name, as defined by columns.nameDT.
        /// </summary>
        public string name { get; set; }

        public bool searchable { get; set; }

        public bool ordertable { get; set; }

    }
}
