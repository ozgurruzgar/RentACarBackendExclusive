using Core.Utilities.IoC;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.DependencyInjection;
using ServiceStack.Redis;
using ServiceStack.Text;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;

namespace Core.CrossCuttingConcerns.Caching.Redis
{
    public class RedisCacheManager : ICacheManager
    {
        private readonly IDistributedCache _distributedCache;
        private readonly DistributedCacheEntryOptions _options;
        public RedisCacheManager()
        {
            _distributedCache = ServiceTool.ServiceProvider.GetService<IDistributedCache>();
            _options.AbsoluteExpiration = DateTime.UtcNow.AddMinutes(60);
        }
        public async Task<object> Get(string key)
        {
            var result = await _distributedCache.GetAsync(key);
            Task.CompletedTask.Wait();
            return result;
        }

        public async Task Add(string key, byte[] value)
        {
            await _distributedCache.SetAsync(key, value, _options);
        }

        public bool IsAdd(string key)
        {
            var result = _distributedCache.Get(key);
            if(result != null)
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
