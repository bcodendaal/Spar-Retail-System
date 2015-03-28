using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;
using Spar.Retail.UI.Models.ViewModels.DataTables;
using SparRetail.Models;

namespace Spar.Retail.UI.Models.ViewModels
{
    /// <summary>
    /// Class that encapsulates most common parameters sent by DataTables plugin
    /// </summary>
    public class DataTableParam
    {
        /// <summary>
        /// Request sequence number sent by DataTable,
        /// same value must be returned in response
        /// </summary>       
        public Dictionary<string, string> additionalParams { get; set; }

        /// <summary>
        /// Text used for filtering
        /// </summary>
        public Dictionary<string, string> search { get; set; }

        /// <summary>
        /// First record that should be shown(used for paging)
        /// </summary>
        public int start { get; set; }

        /// <summary>
        /// Number of columns in table
        /// </summary>
        public int length { get; set; }

        /// <summary>
        /// Number of columns that are used in sorting
        /// </summary>
        public List<DataTableOrder> order { get; set; }

        ///<summery>
        /// Draw counter to sync ajax calls
        /// </summery>
        public int draw { get; set; }

        public List<DataTableColumn> columns { get; set; }

        public string OrderBy
        {
            get { return columns[order[0].column].data; }
        }

        public int OrderDirection
        {
            get { return string.Equals(order[0].dir, "asc", StringComparison.InvariantCultureIgnoreCase) ? 1 : 0; }
        }

        public int PageNumber
        {
            get { return ((start / length) + 1); }
        }

        public int PageSize
        {
            get { return length; }
        }

        public string SearchText
        {
            get { return search["value"]; }
        }

    }
}