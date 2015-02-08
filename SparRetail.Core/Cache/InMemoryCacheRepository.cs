using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Caching;
using System.Text;
using System.Threading.Tasks;

namespace SparRetail.Core.Cache
{
    public class InMemoryCacheRepository : ICacheRepository
    {
        protected readonly ObjectCache cache;

        public InMemoryCacheRepository()
        {
            cache = MemoryCache.Default;
        }

        public T Get<T>(string cacheKey)
        {
            return (T)cache.Get(cacheKey);
        }

        public T Set<T>(string cacheKey, T cacheObject, int durationMinutes)
        {
            cache.Add(cacheKey, cacheObject, new CacheItemPolicy() { AbsoluteExpiration = DateTimeOffset.Now.AddMinutes(durationMinutes) });
            return cacheObject;
        }
    }
}
