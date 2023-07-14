using Azure;
using Castle.DynamicProxy;
using Core.Entities;
using Core.Utilities.IoC;
using Core.Utilities.Results;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Text.Json.Serialization;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
namespace Core.CrossCuttingConcerns.Caching.Redis
{
    public class RedisCacheManager : ICacheManager 
    {
        private readonly IDistributedCache _distributedCache;
        public RedisCacheManager()
        {
            _distributedCache = ServiceTool.ServiceProvider.GetService<IDistributedCache>();
        }

        public async Task Add(string key, object value)
        {
            DateTimeOffset absoluteExpiration = DateTimeOffset.Now.AddHours(1);
            DistributedCacheEntryOptions options = new DistributedCacheEntryOptions() { AbsoluteExpiration = absoluteExpiration };
            var json = JsonConvert.SerializeObject(value, new JsonSerializerSettings() { ReferenceLoopHandling = ReferenceLoopHandling.Ignore });
            byte[] serializeData = Encoding.UTF8.GetBytes(json);
            await _distributedCache.SetAsync(key, serializeData,options);
            Task.CompletedTask.Wait();
        }

        public async Task<SuccessDataResult<TResponse>>? Get<TResponse>(string key)
        {
            byte[]? cacheResponse = await _distributedCache.GetAsync(key);
            if(cacheResponse != null)
            {
               var deserializeData = Encoding.UTF8.GetString(cacheResponse);
               var response = JsonConvert.DeserializeObject<SuccessDataResult<TResponse>>(deserializeData, new JsonSerializerSettings(){ ReferenceLoopHandling = ReferenceLoopHandling.Ignore });
                return response ;
            }
            return null;
        }

        public bool IsAdd(string key)
        {
            var result = _distributedCache.Get(key);
            if(result != null)
            {
                return false ;
            }
            return true;
        }

        public async Task Remove(string key)
        {
           await _distributedCache.RemoveAsync(key);
        }

    }
}
