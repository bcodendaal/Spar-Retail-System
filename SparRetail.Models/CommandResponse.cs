using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SparRetail.Models
{
    public class CommandResponse<T> where T: class
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
        public T Model { get; set; }
    }
}
