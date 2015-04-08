using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SparRetail.Models
{

    public class Page
    {
        public string SearchText { get; set; }
        public string OrderBy { get; set; }
        public int OrderDirection { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public int RowCount { get; set; }
    }

    public class Page<T> : Page
    {
        public Page()
        {
        }

        public Page(Page page, List<T> results)
        {
            Results = results;
            SearchText = page.SearchText;
            OrderBy = page.OrderBy;
            OrderDirection = page.OrderDirection;
            PageNumber = page.PageNumber;
            PageSize = page.PageSize;
            RowCount = page.RowCount;
        }

        public List<T> Results { get; set; }
    }
}
