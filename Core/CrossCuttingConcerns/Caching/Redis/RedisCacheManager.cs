using Core.Utilities.IoC;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.DependencyInjection;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Core.CrossCuttingConcerns.Caching.Redis
{
    public class RedisCacheManager : ICacheManager
    {

        private IDistributedCache _distributedCache;
        DistributedCacheEntryOptions _options;
        public RedisCacheManager(IDistributedCache distributedCache)
        {
            _distributedCache = distributedCache;
            _options.AbsoluteExpiration = DateTime.Now.AddMinutes(60);
        }

        public async Task Add(string key, RedisValue value)
        {
           await _distributedCache.SetAsync(key, value,_options);
        }

        public async Task<object> Get(string key)
        {
            var result = _distributedCache.GetAsync(key);
            return result;
        }

        public async Task<bool> IsAdd(string key)
        {
            var proceed = await _distributedCache.GetAsync(key);
            var result = proceed.Any();
            if(result == null)
            {
                return false;
            }
            return true;
        }
        public async Task Remove(string key)
        {
           await _distributedCache.RemoveAsync(key);
        }

    }
}
