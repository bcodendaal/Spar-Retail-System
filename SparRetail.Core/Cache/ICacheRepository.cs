using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SparRetail.Core.Cache
{
    public interface ICacheRepository
    {
        T Get<T>(string cacheKey);
        T Set<T>(string cacheKey, T cacheObject, int durationMinutes);
    }
}
