using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Caching;
using System.Text;
using System.Threading.Tasks;

namespace SparRetail.Core.Tests
{
    [TestFixture]
    public class CacheTests
    {
        [TestCase]
        public void cache()
        {
            ObjectCache cache = MemoryCache.Default;

            cache.Get("somekey");


        }
    }
}
