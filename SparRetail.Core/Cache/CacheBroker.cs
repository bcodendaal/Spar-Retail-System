using SparRetail.Core.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SparRetail.Core.Cache
{
    public class CacheBroker : ICacheBroker
    {
        protected readonly ICacheRepository cacheRepository;
        protected readonly ILogger logger;

        private const string TagGroup = "CacheBroker";

        public CacheBroker(ICacheRepository cacheRepository, ILogger logger)
        {
            this.cacheRepository = cacheRepository;
            this.logger = logger;
        }

        public T Get<T>(string cacheKey) where T : class
        {
            return cacheRepository.Get<T>(cacheKey);
        }

        public T Set<T>(string cacheKey, T cacheObject, Volatility volatility) where T : class
        {
            return cacheRepository.Set<T>(cacheKey, cacheObject, GetDuration(volatility));
        }

        public T TryGet<T>(string cacheKey, Func<T> getObjectsForCache, Volatility volatility) where T : class
        {
            try
            {
                return Get<T>(cacheKey) ?? Set<T>(cacheKey, getObjectsForCache.Invoke(), volatility);
            }
            catch (Exception ex)
            {
                logger.Error(TagGroup, "TryGet", ex);
                return default(T);
            }
        }
        
        private int GetDuration(Volatility volatility)
        {
            switch (volatility)
            {
                case Volatility.Low:
                    return 1440;
                case Volatility.Medium:
                    return 60;
                case Volatility.High:
                    return 10;
                default:
                    return 10;
            }
        }
    }
}
