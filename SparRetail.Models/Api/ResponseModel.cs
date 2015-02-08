using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SparRetail.Models.Api
{
    public class ResponseModel
    {
        public bool IsCallSuccess { get; set; }
        public bool IsCommandSuccess { get; set; }
        public string Message { get; set; }
    }
}
