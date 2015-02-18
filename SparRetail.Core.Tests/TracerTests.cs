using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SparRetail.Core.Tests
{
    [TestFixture]
    public class TracerTests
    {
        [TestCase]
        public void debugger_output()
        {
            Trace.WriteLine("This is an info message");
            Trace.TraceError("this is an error message I hope");
        }
    }
}
