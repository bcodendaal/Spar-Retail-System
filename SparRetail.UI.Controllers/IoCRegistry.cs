using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace SparRetail.UI.Controllers
{
    public static class IoCRegistry
    {
        public static Assembly GetAssembly()
        {
            return Assembly.GetExecutingAssembly();
        }
    }
}
