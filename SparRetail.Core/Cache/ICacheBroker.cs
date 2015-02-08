using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SparRetail.Core.Cache
{
    public interface ICacheBroker
    {
        T Get<T>(string cacheKey) where T : class;
        T Set<T>(string cacheKey, T cacheObject, Volatility volatility) where T : class;
        T TryGet<T>(string cacheKey, Func<T> getObjectsForCache, Volatility volatility) where T : class;
    }
}
